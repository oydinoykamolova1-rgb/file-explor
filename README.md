# 🌿 Nature File Explorer

A fully functional, beautiful, and nature-inspired full-stack file explorer clone application. Built with **.NET 10 Web API** for a secure, high-performance backend and **Vue 3 (Vite + TypeScript + Pinia)** for a modern, responsive user interface.

Developed and customized with a premium natural palette (Forest Slate, Moss Emerald, Warm Sand, and Glassmorphism) and tailored specifically for **Oydinoy**.

---

## ✨ Features

- 🔐 **Premium Login Screen:** Customized lock screen with dynamic time/date display, profile avatar, password toggling, and bounce animation feedback.
- 🗂️ **Drive Detection:** Lists local system drives (C:, D:, etc.) with visual storage analysis charts powered by `Chart.js`.
- 📂 **Full Directory Browsing:** Click and navigate through system drives, folders, and subfolders.
- ⬅️ **History Navigation:** Back and forward arrows allow seamless history traversal (similar to Windows Explorer).
- 🍞 **Interactive Breadcrumbs:** Shows the current directory structure and allows quick jumps to any parent directory.
- 🔍 **Live Search:** Fast, server-side filtering and real-time frontend search to find directories and files.
- 📁 **Folder Actions:** Create folders directly from a popup modal, or delete existing ones.
- 📤 **File Operations:** Upload files, download files, and delete files with robust error handling.
- 🖱️ **Context Menu:** Right-click context menus for files and folders (Open, Download, Delete).

---

## 🎨 Design System (Nature Colors)

This application has been redesigned from the ground up to reflect a premium nature aesthetic:
* **Backgrounds:** Rich Forest Slate (`#0c1916`) and deep moss greens (`#081210`).
* **Accents & Highlights:** Moss Emerald (`#10b981`) and warm autumn sand (`#d97706`).
* **Glassmorphism:** Frosted border effects, backing blurs (`backdrop-blur-2xl`), and smooth scale transitions.

---

## 🚀 Running Locally

### Prerequisites
* **.NET SDK 10.0** or later
* **Node.js 18+** and npm

---

### 1. Run Backend (.NET 10 API)

1. Open your terminal in the backend API directory:
   ```bash
   cd FileExploler.BackEnd/FileExploler.Api
   ```
2. Run the application:
   ```bash
   dotnet run --launch-profile https
   ```
3. The API will start on:
   - **HTTPS:** `https://localhost:7157`
   - **HTTP:** `http://localhost:5242`

---

### 2. Run Frontend (Vue 3 + Vite)

1. Open a new terminal in the frontend directory:
   ```bash
   cd FileExploler.FrontEnd
   ```
2. Install dependencies:
   ```bash
   npm install
   ```
3. Start the Vite dev server:
   ```bash
   npm run dev
   ```
4. Access the web application in your browser:
   - **URL:** [http://localhost:5173](http://localhost:5173)

---

## 🔐 Credentials

Use the following credentials on the login screen to access the explorer:

* **Username:** `Oydinoy`
* **Password:** `12345678`

---

## 🛠️ Architecture

* **Backend:** Clean Architecture.
  - `FileExploler.Api` - REST Controllers (Endpoints for drives, directories, and files).
  - `FileExploler.Application` - Business logic, models, and abstraction interfaces.
  - `FileExploler.Infrastructure` - Hard disk and file system operations (Safe IO, CORS policies, AutoMapper profiles).
* **Frontend:** Vue 3 component-driven SPA using Pinia for global state management.
