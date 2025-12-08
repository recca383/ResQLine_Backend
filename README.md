# 🚑 ResQLine Backend

ResQLine is an **AI-powered San Pablo City, Laguna–specific emergency hotline** that allows users to send real-time incident reports directly to responders. It also includes an **SOS button** that instantly transmits the user's precise location for immediate assistance.

---

## 📌 Features
- **User Registration & Authentication**
  - Secure login and registration flow.
  - JWT-based authentication.

- **Emergency Report Submission**
  - Users can send reports (text, images, metadata).
  - Real-time dispatching to responders.

- **SOS Location Trigger**
  - Sends precise user coordinates instantly.
  - Integrates with responder dashboard.

- **AI-Assisted Categorization**
  - AI classifies report severity and type.
  - Helps responders prioritize incidents.

- **Admin & Responder Tools**
  - View, manage, and update reports.
  - Real-time status tracking.
  
- ✔️ C# / .NET Backend API  
- ✔️ REST API for mobile, web, and admin clients  
- ✔️ Clean layered structure (Application, Domain, Infrastructure, SharedKernel, Presentation/Web.Api)  
- ✔️ Docker & Docker Compose support  
- ✔️ Architecture tests  
- ✔️ Follows SOLID and clean architecture practices  

## 🛠️ Tech Stack

| Layer | Technology |
|-------|------------|
| Backend Framework | **.NET 9 Minimal API** |
| Database | **PostgreSQL** |
| Authentication | **JWT** |
| SMS Provider | Semaphore |
| Hosting | Render |

---

## 📁 Project Structure
``` 
ResQLine_Backend/
│
├── src/ # Main backend source code
│ ├── Application/ # Business Logic / DTOs
│ ├── Domain/ # Domain Models / Domain Events
│ ├── Infrastucture/ # External Libraries / Persistence / API-to-API Communication
│ ├── SharedKernel/ # Shared classes 
│ ├── Web.Api/ # API endpoints
│ └── Program.cs # App entry point
│
├── tests/
│ └── Layers/ # Architecture, naming & structure tests
│ 
│
├── docker-compose.yml # Docker orchestration
├── docker-compose.override.yml
├── README.md
└── ResQLine.sln # .NET solution file
```
---

## 🛠️ Getting Started

### Prerequisites

Install the following:

- .NET SDK  
- Docker Desktop (optional but recommended)  
- Git  

---

## 🚀 Running the Backend

### 1️⃣ Running Locally

Restore dependencies:

```bash
dotnet restore
```
Build:
```bash
dotnet build
```
Run:
```bash
dotnet run --project src/ResQLine
```
API will be available at:
```
http://localhost:5000
https://localhost:7000
```
Configuration files (Not in Github Repo):
```
src/ResQLine/.env
```

### 🙌 Acknowledgements
Thank you to all contributors helping improve ResQLine Backend.
