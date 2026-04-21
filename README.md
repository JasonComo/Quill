# Quill
Quill is an AI-assisted product planning app that helps users turn app ideas into user stories and generate downloadable HTML mockups for individual features.
The app includes an ASP.NET Core backend, a Blazor frontend, cookie-based authentication, user-owned app/story data, SQLite persistence, and OpenAI-powered mockup generation.
## Features
- Register, log in, and log out with ASP.NET Core Identity cookie authentication
- Create, edit, view, and delete app ideas
- Define app metadata such as app type, field, target user, purpose, and notes
- Create, edit, view, and delete user stories for each app
- Generate downloadable HTML mockups from stories using OpenAI
- Store generated mockups and track generation status
- Download generated mockups as standalone HTML files
## Tech Stack
### Backend
- ASP.NET Core / .NET 9
- Entity Framework Core
- SQLite
- ASP.NET Core Identity
- OpenAI Responses API
- Repository pattern
- DTOs and mapper classes
### Frontend
- Blazor Server
- Razor components
- Cookie-authenticated API calls
- Custom CSS styling
## Project Structure
```text
QuillApp/          ASP.NET Core API
QuillApp.Blazor/   Blazor Server frontend
QuillApp.sln       Solution file
```
## How It Works
1. A user registers or logs in.
2. The user creates an app idea with product context such as purpose, field, type, and target audience.
3. The user adds stories for that app.
4. From a story, the user clicks Generate Mockup.
5. The backend sends the app and story context to OpenAI.
6. OpenAI returns a standalone HTML document.
7. The backend stores the generated mockup.
8. The user can download the mockup as an `.html` file.
## AI Mockup Generation
The OpenAI integration is handled on the backend. The frontend sends story context to the API, and the API handles mockup generation.
The prompt includes:
- app name
- app purpose
- app type
- field
- target user
- additional app notes
- story title
- story description
- acceptance criteria
- optional generation notes
## Local Setup
### Prerequisites
- .NET 9 SDK
- Git
- An OpenAI API key
### 1. Clone the repository
```powershell
git clone https://github.com/YOUR_USERNAME/Quill.git
cd Quill
```
### 2. Configure the OpenAI API key
From the API project folder:
```powershell
cd QuillApp
dotnet user-secrets set "OpenAI:ApiKey" "your-api-key-here"
cd ..
```
Do not put your API key in `appsettings.json`.
### 3. Create the database
Run the EF Core migrations:
```powershell
dotnet ef database update --project .\QuillApp\QuillApp.csproj --startup-project .\QuillApp\QuillApp.csproj
```
### 4. Run the API
```powershell
dotnet run --project .\QuillApp\QuillApp.csproj
```
By default, the Blazor app expects the API at:
```text
http://localhost:5079/
```
### 5. Run the Blazor frontend
In a second terminal:
```powershell
dotnet run --project .\QuillApp.Blazor\QuillApp.Blazor.csproj
```
Open the displayed Blazor URL in your browser.
## Configuration
The API includes an example development settings file:
```text
QuillApp/appsettings.Development.example.json
```
Local development settings can be placed in:
```text
QuillApp/appsettings.Development.json
```
## Future Improvements
- Generation progress/status updates
- In-app HTML preview
- User-configurable mockup styles
- Dockerized local development
## Why I Built This
I built Quill as a way to expand my .NET skills beyond a local CRUD app. I wanted to implement meaningful authentication and a more complete full-stack workflow with a real backend, frontend, and external API integration.

The OpenAI integration was a fun way to explore something I had not worked with before while still keeping the project grounded in a practical use case. Instead of only storing app ideas and stories, Quill turns those stories into downloadable HTML mockups that can be inspected, shared, and iterated on.

The goal was to build something that felt closer to a real product workflow than a standard practice app.
## Screenshots
### Login

<img width="3835" height="1887" alt="Screenshot 2026-04-20 190853" src="https://github.com/user-attachments/assets/dc1c1d65-2024-46ce-ba0f-dae1c80676f6" />

### Apps

<img width="3822" height="1874" alt="Screenshot 2026-04-20 190912" src="https://github.com/user-attachments/assets/0cf8dfb4-8657-44f9-8cbd-74e23598cf6b" />

<img width="3819" height="1883" alt="Screenshot 2026-04-20 191016" src="https://github.com/user-attachments/assets/65a6db33-b8ff-4ed9-957a-c3f1da8ce25d" />

<img width="3769" height="1829" alt="Screenshot 2026-04-20 191037" src="https://github.com/user-attachments/assets/9f160ac2-812d-4854-babf-c7ae1d588440" />

### Stories

<img width="3809" height="1883" alt="Screenshot 2026-04-20 191050" src="https://github.com/user-attachments/assets/4121f963-42e2-404a-af6f-ba740f46c601" />

<img width="3803" height="1865" alt="Screenshot 2026-04-20 191145" src="https://github.com/user-attachments/assets/0cb9d9d1-21cc-4fe6-b89f-db437f77872b" />

<img width="3813" height="1878" alt="Screenshot 2026-04-20 191459" src="https://github.com/user-attachments/assets/a3d3f6b6-70b0-4337-a661-aad16aa2e13d" />

### Mockup

<img width="3752" height="1856" alt="Screenshot 2026-04-20 192926" src="https://github.com/user-attachments/assets/ce00df8d-fe5e-4042-9deb-970e47752995" />




