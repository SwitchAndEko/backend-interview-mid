# 後端工程師技術測試

## 專案簡介

本專案為 .NET 8 Web API，針對 SQL Server 資料庫中的 `MyOffice_ACPD` 資料表實作完整 CRUD 功能，並提供 Swagger UI 進行 API 測試。

## 技術框架

- .NET 8 Web API
- Entity Framework Core 8 (SQL Server)
- Swashbuckle (Swagger/OpenAPI)
- SQL Server 2019+

## 專案架構

```
Interview/
└── Interview.API/
    ├── Controllers/
    │   ├── GenericController.cs          # 基底 Controller，提供統一回應格式與錯誤處理
    │   └── MyOfficeAcpdController.cs     # 使用者 CRUD Controller
    ├── Filters/
    │   └── SwaggerExampleSchemaFilter.cs  # Swagger 測試資料範例
    ├── Models/
    │   ├── Entities/                      # EF Core Power Tools 自動產生的實體模型
    │   │   ├── BackendExamHub_Context.cs  # DbContext
    │   │   ├── MyOffice_ACPD.cs           # ACPD 資料表實體
    │   │   └── MyOffice_ExcuteionLog.cs   # 執行日誌實體
    │   ├── Failures/
    │   │   └── BaseFailure.cs             # 統一錯誤回應物件
    │   ├── Requests/
    │   │   ├── AddUserRequest.cs          # 新增使用者請求 DTO（含驗證）
    │   │   └── UpdateUserRequest.cs       # 更新使用者請求 DTO（含驗證）
    │   └── Responses/
    │       └── UserResponse.cs            # 使用者回應 DTO
    ├── Program.cs                         # 應用程式進入點與服務註冊
    └── GlobalUsing.cs                     # 全域 using 宣告
TSQLScript/
    ├── TSQL_Myoffice_ACPD.sql             # ACPD 資料表 DDL
    ├── TSQL_Myoffice_ExcuteionLog.sql     # 執行日誌資料表 DDL
    ├── NewSID_自訂一組固定欄位的代碼.sql     # NEWSID 預存程序（主鍵產生）
    └── usp_AddLog 記錄執行錯誤.sql          # usp_AddLog 預存程序（錯誤日誌）
```

## API 端點

| HTTP Method | URL | 說明 | 回應狀態碼 |
|---|---|---|---|
| `GET` | `/api/myofficeacpd` | 查詢所有資料 | 200 / 500 |
| `GET` | `/api/myofficeacpd/{id}` | 查詢單筆資料 | 200 / 400 / 404 / 500 |
| `POST` | `/api/myofficeacpd` | 新增資料 | 201 / 400 / 500 |
| `PUT` | `/api/myofficeacpd/{id}` | 更新資料 | 200 / 400 / 404 / 500 |
| `DELETE` | `/api/myofficeacpd/{id}` | 刪除資料 | 204 / 400 / 404 / 500 |

## 設計說明

- **Request/Response DTO**：API 不直接暴露資料庫實體，透過 `AddUserRequest`、`UpdateUserRequest` 接收輸入，`UserResponse` 回傳結果
- **資料驗證**：Request DTO 使用 `DataAnnotations` 驗證（`StringLength`、`EmailAddress`），由 `[ApiController]` 自動觸發，回傳 400 Bad Request
- **主鍵產生**：新增資料時透過 `NEWSID` 預存程序產生 `ACPD_SID`
- **錯誤日誌**：所有 catch 區塊透過 `usp_AddLog` 預存程序將錯誤寫入 `MyOffice_ExcuteionLog` 資料表
- **統一回應格式**：透過 `GenericController` 的 `GenericContent()` 方法統一處理成功與失敗回應

## 執行步驟

### 前置需求

- .NET 8 SDK
- SQL Server 2019+
- Visual Studio 2022（建議）

### 資料庫設定

1. 在 SQL Server 中建立資料庫
2. 依序執行 `TSQLScript/` 目錄下的 SQL 腳本建立資料表與預存程序
3. 修改 `Interview/Interview.API/appsettings.json` 中的連線字串：

```json
{
  "ConnectionStrings": {
    "BackendExamHub": "Server=你的伺服器;Database=你的資料庫;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 啟動專案

```bash
# 使用 CLI 啟動（HTTP）
dotnet run --project Interview/Interview.API

# 使用 CLI 啟動（HTTPS）
dotnet run --project Interview/Interview.API --launch-profile https
```

或在 Visual Studio 2022 中按 **F5** 即可啟動，瀏覽器會自動開啟 Swagger UI。

### Swagger 測試

- HTTP：`http://localhost:5220/swagger`
- HTTPS：`https://localhost:7147/swagger`

Swagger UI 中每個 API 端點皆已預填測試用 JSON 資料，可直接點選 "Try it out" 進行測試。

## Git 分支管理

- `main`：最終版本
- `feature/implement-crud-api`：CRUD API 功能開發分支
