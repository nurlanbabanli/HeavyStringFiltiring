# HeaveStringFiltering

## 📌 Overview

**HeaveStringFiltering** is a layered .NET 8 Web API application that receives chunks of strings via a `POST` endpoint and applies custom filtering based on the **Levenshtein distance algorithm**, which is implemented from scratch without any third-party libraries. This allows the application to identify and extract strings that are similar based on configurable thresholds — making it suitable for fuzzy string matching or deduplication scenarios.

---

## 🏗️ Architecture

The solution follows a clean **layered architecture**, promoting separation of concerns, scalability, and testability.

### 📂 Project Structure

```
/HeaveStringFiltering.sln
│
├── Api         → ASP.NET Core Web API (Startup project)
├── Business    → Core business logic, including the Levenshtein filtering service
├── Dtos        → Data Transfer Objects shared between API and business logic
├── Tests       → NUnit unit tests for business logic and API
```

### 🧩 Project Roles

- **Api**:  
  Exposes HTTP endpoints, including the `POST` method for receiving string chunks. It orchestrates requests and responses.

- **Business**:  
  Implements all core logic, including the custom Levenshtein distance algorithm and filtering workflow. Acts as the main processing layer.

- **Dtos**:  
  Contains Data Transfer Objects used for input/output between layers, ensuring decoupling and validation.

- **Tests**:  
  Includes NUnit-based unit tests focused on verifying business logic correctness, particularly around filtering and edge cases.

  ---

## 🔍 Filtering Logic: Levenshtein Distance

Filtering is based on **Levenshtein distance**, a metric for measuring the difference between two sequences by counting the minimum number of edits (insertions, deletions, substitutions) needed to transform one string into the other.

### ✅ Highlights

- **Pure C# implementation** (no external libraries)
- Efficient for long text chunks
- Adjustable distance threshold for flexible matching

### 🧮 Example Implementation

## 🚀 How to Run the Application

### ✅ Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Visual Studio 2022 **or** Visual Studio Code

---

### 🖥️ Running via Visual Studio 2022

1. Open `HeaveStringFiltering.sln` in Visual Studio 2022.
2. Right-click the `Api` project and select **Set as Startup Project**.
3. Press `F5` or click **Start Debugging**.

> The API will start and listen on ports like `https://localhost:5000`.

---

### 💻 Running via Visual Studio Code

1. Open the project folder in VS Code.
2. Ensure .NET 8 is installed:

```bash
dotnet --version
```

3. Navigate to the API project and run it:

```bash
cd Api
dotnet restore
dotnet run
```

> By default, the API will be available at `http://localhost:5000` or `https://localhost:5001`.

---

## 🧪 How to Test the Application

This solution uses **NUnit** for unit testing.

### ✅ In Visual Studio 2022

1. Open the solution.
2. Open **Test Explorer** (`Test > Test Explorer`).
3. Click **Run All Tests**.

### ✅ In VS Code or CLI

```bash
cd Tests
dotnet test
```

---
## 🧪 NUnit Testing Requirements

To run unit tests with **NUnit**, ensure your test project includes the following:

### ✅ Required NuGet Packages

Install these packages in your test project:

```bash
dotnet add package NUnit
dotnet add package NUnit3TestAdapter
dotnet add package Microsoft.NET.Test.Sdk