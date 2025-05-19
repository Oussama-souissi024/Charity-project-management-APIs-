# 🌍 Charity Project Management APIs

A **.NET 6 Web API** solution for managing charity-driven city projects, donations, materials, transportation, and user roles.  The system is split into three layers:

1. **CityProjects** – ASP.NET Core Web API (controllers, DI, startup)
2. **CityProjects.Core** – Domain models & DTOs
3. **CityProjects.Data** – Entity Framework Core data-access layer

> Designed to help NGOs or municipalities track projects, mandates, and resources transparently.

---

## ✨ Key Features

| Area | Description |
|------|-------------|
| **Authentication / Roles** | Register / login endpoints, role management (`CityUserRole`) |
| **Projects** | CRUD for projects; track budget, status, region |
| **Donations** | Capture monetary & in-kind donations, link to projects |
| **Materials & Transportations** | Inventory of materials and transport resources |
| **Mandates** | Authorised mandates for spending / activities |
| **Regions** | Geographic classification of projects |
| **Auto-Mapping** | `Mapper/` profile classes map between entities & DTOs |
| **Swagger** | Auto-generated docs once the API is running |

---

## 🗂️ Solution Structure

```
Charity-project-management-APIs--main
│
├── CityProjects/                # ASP.NET Core Web API
│   ├── Controllers/             # REST endpoints (Projects, Donations, Users…)
│   ├── Program.cs               # .NET 6 minimal hosting model
│   ├── appsettings*.json        # Config & connection string
│   └── wwwroot/                 # Static files (if any)
│
├── CityProjects.Core/           # Domain layer
│   ├── *.cs                     # POCO entities & view-models
│   └── Mapper/                  # AutoMapper profiles
│
├── CityProjects.Data/           # Data-access layer
│   ├── ApplicationDbContext.cs  # EF Core DbContext
│   └── Repositories/           
│
└── CityProjects.sln             # Visual Studio solution
```

---

## 🛠 Prerequisites

- **.NET 6 SDK** (or newer)
- **SQL Server** or **LocalDB** (adjust connection string in `appsettings.json`)
- **Visual Studio 2022** / VS Code / Rider

---

## 🚀 Getting Started

1. **Clone** the repository:
   ```bash
   git clone https://github.com/<your-org>/Charity-project-management-APIs.git
   cd Charity-project-management-APIs--main
   ```
2. **Configure** the database connection string in `CityProjects/appsettings.json`.
3. **Run EF Core migrations** (optional if `CityProjects.Data` has migrations):
   ```bash
   dotnet ef database update --project CityProjects.Data
   ```
4. **Launch** the API:
   ```bash
   dotnet run --project CityProjects
   ```
5. Navigate to `https://localhost:5001/swagger` to explore endpoints.

---

## 🧪 Running Tests

If unit / integration tests exist under `Tests/`:
```bash
dotnet test
```

---

## 🛡 Security & Auth

The API uses JWT bearer tokens (or standard cookie auth depending on config).  
Use `/api/auth/register` and `/api/auth/login` (see controllers) to obtain tokens.

---

## 🖇️ API Endpoints (excerpt)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/projects` | List all projects |
| POST | `/api/projects` | Create a new project |
| PUT | `/api/projects/{id}` | Update project |
| DELETE | `/api/projects/{id}` | Remove project |
| GET | `/api/donations` | List donations |
| POST | `/api/materials` | Add material item |
| POST | `/api/auth/login` | Obtain JWT token |

Full list available in **Swagger UI**.

---

## 🤝 Contributing

1. Fork & clone
2. Create a feature branch: `git checkout -b feature/awesome`
3. Commit & push: `git commit -m "feat: awesome"`
4. Open a Pull Request

---

## 📜 License

Open-source project – feel free to use, modify, and distribute under the terms of the LICENSE file (or MIT by default).

---

## 👤 Author

**Oussama Souissi** – [GitHub](https://github.com/Oussama-souissi024)
