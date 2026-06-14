# IT Asset Manager

IT Asset Manager is a **.NET 9 Blazor** application for managing enterprise IT assets and operations in one place.

## Core Features

- **Asset inventory management** for:
  - Servers
  - Storage
  - Network equipment
  - Security equipment
  - Software
  - Support equipment
  - Miscellaneous equipment
- **Lifecycle and operations tracking**:
  - Failures (faults)
  - Security vulnerabilities
  - Routine checks
  - Maintenance history
- **SLA management and dashboard analytics**:
  - Uptime rate
  - Disability/fault handling time
  - Technical support completion rate
  - Routine check compliance rate
  - Security issue trends
  - Aggregated SLA scoring
- **Schedule calendar** with rich-text details and user-based appointment data
- **Operations document pages** with database-backed text editing and saving for:
  - Work Request
  - Routine Check
  - Regular PM
  - Operation Manual
  - System Status
  - Emergency Contact
  - Etc.
- **Admin features**:
  - User and role management
  - Excel import/export for major asset datasets

## Security and Access

- ASP.NET Core Identity-based authentication
- Role/policy authorization (Administrator/User)
- Protected pages and admin-only menus

## Technical Stack

- **Frontend/UI**: Blazor + Blazorise
- **Backend**: ASP.NET Core (Blazor Web App)
- **Database**: Entity Framework Core with MySQL
- **Target Framework**: .NET 9

## Solution Structure

- `ITAssetManager` - Server host, identity, EF Core, app startup
- `ITAssetManager.Client` - Client-side pages/routes
- `ITAssetManagerComponents` - Reusable UI components
- `ITAssetManagerLibrary` - Shared models, constants, helpers, DbContext
