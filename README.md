# COMP_2139
# Assignment 2 – Inventory Management System

 Student Information
- Name: Fredrich Tan
- Student ID: 101318950


 Live Application



Technologies Used
- ASP.NET Core MVC
- Entity Framework Core (with PostgreSQL)
- Identity for authentication
- Razor Views (CSHTML)
- Serilog for logging
- Azure App Service deployment
- GitHub for source control

 Features Implemented
-  User Registration / Login (Identity)
-  Email Confirmation via FakeEmailSender
-  PostgreSQL database integration (Supabase)
- CRUD functionality for Inventory Items
-  MVC Structure (Models, Views, Controllers)
- eployment to Azure App Service
- Logging with Serilog
-  Form validation, error handling, and navigation

Setup Instructions
1. Clone the repo:
   ```bash
   git clone https://github.com/Cloudtemplar2615/assignment2-inventory.git

 Deployment & Configuration Status Report
Student Name: Fredrich Tan
 Student ID: 101318950
 Course: COMP2139 – Web Application Development
 Assignment: Assignment 2 – Smart Inventory Management System (Version 2)
 Date: April 12, 2025

Overview
This report documents the setup, configuration, deployment, and feature completion status for the Smart Inventory Management System, deployed to Azure and backed by Supabase PostgreSQL.

 Assignment Requirements Checklist
Requirement
Status
ASP.NET Core MVC Project
       -Complete
PostgreSQL Integration (Supabase)
       -Complete
Entity Framework Core + Migrations
      -Applied to Supabase
Role-based Authentication (Admin/User)
    -Implemented
Enhanced ASP.NET Identity
    - Customized
User Registration with Email Confirmation
    -Working
Role Management (Admin creates roles/users)
   -Implemented
Product CRUD with Low Stock Alerts
   -Implemented
Dynamic Search using AJAX
   -Working
ProjectTasks with related CRUD
   -Complete
Project Summary ViewComponent
   -Added
Custom Error Handling (Global + Pages)
   -Done
Logging with Serilog
   -Working
Deployment to Azure
   -Live
Supabase Testing + Local Testing
   -Verified
Unit Testing with xUnit
   -Added


Features Implemented
Identity and Access
ASP.NET Identity integrated with PostgreSQL


Admin/User roles created with custom FullName, ContactNumber, and PreferredCategories fields


Admin dashboard with user management access


Product Management
CRUD operations for Products


Low Stock threshold triggers a visual alert


AJAX-based dynamic product search with real-time spinner


Project & Tasks
Project entity extended with StartDate, EndDate, and Status


Each Project links to multiple ProjectTasks with full CRUD
Project summary data displayed via ViewComponent


Technical
Custom error pages (404, 500)


Global exception handling with UseExceptionHandler and logging


Serilog configured for file and console output


xUnit tests created for models and key services



 Supabase PostgreSQL Configuration
Supabase PostgreSQL instance created


Connection string added to appsettings.json


EF Core migrations applied successfully via dotnet ef database update



 Deployment to Azure
Azure Web App: Assignment2-Inventory


Region: Canada Central


Runtime: Windows + .NET 8


Connected GitHub Actions CI/CD pipeline


App starts successfully (verified via Log Streaming)


 Live Site
https://assignment2-inventory-ctgvcdbgheb7ddce.canadacentral-01.azurewebsites.net

GitHub Repository
🔗 https://github.com/Cloudtemplar2615/Assignment2_2139


