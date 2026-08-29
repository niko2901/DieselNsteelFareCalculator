# Diesel n' Steel Fare Calculator

A lightweight fare calculator for the Roblox game **Diesel N Steel**. This app helps you quickly estimate base fare, total fare, and change for selected routes and passenger settings.

## Features

- Route-based fare calculation for:
  - Bulakan-Balagtas
  - Bulakan-Guiguinto
  - Bulakan-Malolos (with direction toggle)
- Passenger count support (1-5)
- Discount mode for Student/Senior passengers
- Quick bill selection (20, 50, 100) with automatic change calculation

## Tech Stack

- **C#** (.NET 8, ASP.NET Core Blazor)
- **HTML** (Razor component markup)
- **CSS** (custom styles + Bootstrap)

## Usage

## Live Website

- [Open Diesel n' Steel Fare Calculator](https://dieselnsteelfarecalculator-ach2c6exa8dpbybv.japaneast-01.azurewebsites.net)

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)

### Run locally

1. Clone the repository.
2. From the repository root, run:

```bash
dotnet run --project DieselNsteel/DieselNsteel.csproj
```

3. Open the local URL shown in the terminal.
4. Pick a route, choose origin/destination, set passenger and discount options, then check the computed fare and change.

---

This tool is intended for fare estimation in the Roblox game **Diesel N Steel**.
