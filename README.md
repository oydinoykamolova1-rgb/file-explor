# FileExploler

This repository contains a full-stack file explorer application with a .NET backend and a Vue.js frontend.

## Repository structure

- `FileExploler.BackEnd/` — backend solution and API project
- `FileExploler.FrontEnd/` — frontend application built with Vite + Vue 3

## Prerequisites

- .NET SDK 10.0 or later
- Node.js 18+ and npm

## Run locally

### Backend

1. Open terminal in `FileExploler.BackEnd/FileExploler.Api`
2. Run:
   ```bash
   dotnet run --launch-profile https
   ```
3. Backend will start on:
   - `https://localhost:7157`
   - `http://localhost:5242`

### Frontend

1. Open terminal in `FileExploler.FrontEnd`
2. Install dependencies once (if not installed):
   ```bash
   npm install
   ```
3. Start the dev server:
   ```bash
   npm run dev
   ```
4. Open the app in browser at the URL shown by Vite, typically:
   - `http://localhost:5173`

## Notes

- The frontend is configured to call the backend at `https://localhost:7157`.
- If the backend is already running, stop any existing `FileExploler.Api` process before restarting to avoid build file locks.
- If you clone this repository to a new location, run the same commands after cloning.

## Clone and run

```bash
git clone <repository-url> FileExploler.FinalApp
cd FileExploler.FinalApp/FileExploler.BackEnd/FileExploler.Api
dotnet run --launch-profile https
```

In a second terminal:

```bash
cd FileExploler.FinalApp/FileExploler.FrontEnd
npm install
npm run dev
```
