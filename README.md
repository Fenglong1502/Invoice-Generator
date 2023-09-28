# Invoice Generator

Invoice Generator is a foundational .NET Framework 4.6.1 project, designed to serve as a boilerplate for applications requiring PDF invoice generation functionality. Developed using C# and ASP.NET, it provides the essential structure to create an application that can generate and send PDF invoices, accommodating user-defined company logos and business logic.

## 📌 Features
- Generate PDF invoices.
- **Email Functionality:** Automate the sending of generated invoices via email using `System.Net.Mail.SmtpClient`.
- Customizable: Users can input their own company logo.
- Utilizes `iTextSharp.text.Font` for font sizing within the invoice.
- Acts as a starting point for users to implement their own business logic for invoice generation and distribution.

## 🛠️ Technologies
- C#
- .NET Framework 4.6.1
- ASP.NET
- iTextSharp
- System.Net.Mail

## 🚀 Getting Started
To begin with, the project:
1. Navigate to the project directory.
2. Open `Invoice Generator.sln` file.

## 💡 Usage
This project serves as a boilerplate, enabling users to expand upon the existing codebase and structures to tailor the application to their specific needs and business logic for invoice generation and distribution. You are encouraged to modify and adapt the project to create your customized invoice generator.

### Email Configuration
The project has an in-built email functionality that triggers the sending of generated invoices via email using `System.Net.Mail.SmtpClient`. Users must provide `NetworkCredential` with the company's SMTP email address and password for the email functionality to work correctly.

## 🌐 Web Application
Invoice Generator is developed as a web application, facilitating user interactions and inputs through a user-friendly web interface.

## 📄 License
This project is licensed under the MIT License

## ⚠️ Disclaimer
This project is not actively maintained and relies on technologies and practices relevant at the time of its creation. Users are advised to review and update the codebase as necessary to align with current best practices and technology standards.

## 📬 Contact
For any inquiries or clarifications related to this project, please contact [zell_dev@hotmail.com](mailto:zell_dev@hotmail.com).
