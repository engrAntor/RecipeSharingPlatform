# Recipe Sharing API

This is the backend API for a social recipe sharing platform, developed as a final project for my university course. It is built using ASP.NET Web API (.NET Framework) and follows a clean, 3-tier architecture with a strong emphasis on SOLID principles.

## Features

### Core Functionalities
- **Full User Authentication:** Secure user registration with email verification, JWT-based login, and password reset via email OTP.
- **Role-Based Authorization:** Distinction between regular `User` and `Admin` roles.
- **Complete Recipe CRUD:** Authenticated users can create, read, update, and delete their own recipes.
- **Social Following System:** Users can follow and unfollow other recipe creators.

### Advanced (Non-CRUD) Features
- **Personalized News Feed:** A `GET /api/feed` endpoint that returns a chronological list of new recipes from users you follow.
- **Advanced Search & Filtering:** A powerful `GET /api/recipes/search` endpoint to find recipes by keyword, cuisine, category, and preparation time.
- **Recipe Rating & Aggregation:** Users can rate recipes (1-5 stars). The system automatically recalculates the recipe's average rating in real-time.
- **User Notifications:** An event-driven system that sends email alerts for social interactions, such as when someone you follow posts a new recipe or when someone rates your recipe.
- **Personalized Recommendations:** A `GET /api/recommendations` endpoint that suggests recipes to users based on their rating history and social network (e.g., "Because you liked Italian food..." or "Trending recipes from users you follow").

## Architecture & Technology Stack

This project is built with a professional, maintainable N-Tier architecture.

- **`PresentationAPI` (Presentation Layer):**
  - ASP.NET Web API 2 for handling RESTful HTTP requests.
  - **Unity.WebAPI** for Dependency Injection.
  - **ASP.NET Identity & JWT** for token-based authentication and authorization.

- **`BLL` (Business Logic Layer):**
  - Contains all business rules, services, and data transformation logic.
  - **AutoMapper** for converting between DAL models and public-facing DTOs.
  - **MailKit** for sending emails (verification, notifications, password reset).
  - Demonstrates **SOLID principles**, especially the **Open/Closed Principle** in the recommendation engine and the **Single Responsibility Principle** in its services.

- **`DAL` (Data Access Layer):**
  - **Entity Framework 6 (Code-First)** for database modeling and communication.
  - **Repository Pattern** to abstract database operations.
  - **Microsoft SQL Server** as the database.

## How to Run the Project

1.  **Database Setup:**
    - Open the `PresentationAPI/Web.config` and `DAL/App.config` files.
    - Change the `connectionString` to point to your local SQL Server instance.
    - Open the Package Manager Console, set the Default Project to `DAL`, and run the `Update-Database` command to create and seed the database.

2.  **Email Setup:**
    - Open `PresentationAPI/Web.config`.
    - In the `<appSettings>` section, enter your own Gmail address and a 16-character **App Password**.

3.  **Run the Application:**
    - Set the `PresentationAPI` project as the startup project.
    - Press F5 to run. The API will be hosted on a local IIS Express server. Use a tool like Postman to test the endpoints.
