# GymFit+

GymFit+ is a social media platform tailored for fitness enthusiasts, offering a holistic approach to your fitness journey. Create personalized fitness programs, explore a variety of exercises and healthy recipes, and track your progress with comprehensive statistics. The platform allows users to manage their weight, height, and other health-related metrics through an intuitive dashboard.

## Features

- **Personalized Fitness Programs:** Design your own fitness programs tailored to your goals.
- **Exercise Library:** Explore a diverse range of exercises to keep your workouts engaging and effective.
- **Healthy Recipes:** Discover and share nutritious recipes to complement your fitness routine.
- **Dashboard:** Monitor and update your stats, including weight and height, on an interactive dashboard.
- **Future Features:** Planned enhancements include friend networking, chats, posts, and deployment on Azure.

## Tech Stack

- **Backend:** .NET, ASP.NET, Entity Framework Core
- **Languages:** C#, HTML, CSS, JavaScript (Bootstrap, Ajax, jQuery, Vanilla JavaScript)
- **Architecture:**  MVC and Unit of Work Patterns, Service Layer, Data Layer
- **Other Concepts:** Model Binding, ViewModels, Object-Oriented Programming (OOP) principles, Filters, Middlewares
- **Error Handling:** Robust error handling mechanisms for a smooth user experience, server-side and client-side validation
- **Security Measures:** SQL Injection prevention, XSS protection, CSRF prevention, parameter tampering prevention, AutoForgeryToken implementation
- **Dependency Injection:** Utilizes Dependency Injection and IoC Container for modular and scalable design
- **Data Storage:** MS SQL with EF Core for code-first approach; Future plans include MongoDB for image storage
- **Authentication:** ASP.NET Identity for secure user authentication

## Getting Started

To set up the project locally, follow these steps:

### Prerequisites

- [.NET](https://dotnet.microsoft.com/download)
- [Visual Studio](https://visualstudio.microsoft.com/) or any preferred .NET IDE
- [MS SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1. Clone the repository
2. Open the project in Visual Studio.
3. Configure the database connection in appsettings.json or secrets.json.
4. Apply migrations
5. Run the project.

### Usage

1. Navigate to http://localhost:your-port in your web browser.
2. Create an account.
3. Explore the features, create fitness programs, and track your progress on the dashboard.

### Contributing

Feel free to contribute to the project by submitting bug reports, feature requests, or pull requests. Follow the guidelines in the Contribution Guidelines.

### License

This project is licensed under the MIT License.

### Acknowledgments

Special thanks to the .NET community and all contributors who help make GymFit+ better.

### Live Demo

A live demo of GymFit+ will be available in the future.
