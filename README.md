# 💰 Banking Application

Banking Application is a console-based application written in C# that simulates a basic banking system. Users can manage clients, create savings and investment accounts, perform stock purchases, and view client details. The system includes features like input validation, account balance management, and automatic commission calculation for investments.

It follows object-oriented principles and separates logic into services, models, and utility classes.

## 🧠 Overview

This application allows users to:

- Add new customers
- Create savings accounts (with interest rates)
- Create investment accounts
- Buy stocks (with commission calculation)
- View detailed customer and account information
- Navigate via a simple menu system

The application uses in-memory storage and is built using object-oriented programming principles.

## 🧰 Technologies

- Language: **C#**
- Framework: **.NET Console Application**
- Architecture: **OOP (Object-Oriented Programming)**

## 📋 Menu Options

| Option | Description |
|--------|-------------|
| 1      | Add a new customer |
| 2      | Create a savings account |
| 3      | Create an investment account |
| 4      | Purchase stocks (with commission) |
| 5      | Display customer and account details |
| 6      | Exit the application (with confirmation) |

## 💡 Features

- Input validation for names, addresses, balances, and numeric entries
- Commission is calculated dynamically during stock purchases
- Customers can have multiple accounts
- Investment accounts support multiple stock orders
- Fully interactive console interface

## 📌 Example Scenarios

- A customer can open both a savings and an investment account
- Investment account holders can purchase stocks, and the app will deduct the total cost (including commission) from their balance
- You can view a customer’s personal details, all accounts, and any stock orders made

## 📦 How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/aleTomasz/BankingApplication.git

