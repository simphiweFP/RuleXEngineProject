# 🧠 SmartRules — A Strategy-Based Rule Engine

## 📌 Project Description

**SmartRules** is a modular and extensible rule engine built in C# using the **Strategy Design Pattern**. It allows dynamic evaluation of business rules using a single input model. This system is designed to support various domains, with a current focus on **insurance underwriting**, where applicant data is evaluated against multiple configurable and hardcoded business rules.

---

## ❗ Problem This Solves

In real-world systems like insurance, financial services, or compliance:
- Business rules change frequently.
- Rules come from multiple sources (code, config, spreadsheets).
- Rule logic becomes scattered and tightly coupled with business workflows.

This leads to:
- Poor maintainability
- Difficulty testing rules in isolation
- Hardcoding of logic in multiple places

**SmartRules** solves this by:
- Encapsulating each rule in its own class
- Allowing flexible rule injection via DI
- Supporting multiple input sources (config, adapters, hardcoded)

---

## ✨ Features

- ✅ Clean architecture using Strategy Pattern
- 🧩 Plug-and-play rule implementations
- 📄 Single input model for all rules
- 🔁 Evaluates rules from multiple sources:
  - Hardcoded C# classes
  - JSON/appsettings config
  - Adapters for Excel/Google Sheets (simulated)
- ✅ Built-in support for enums, validation, and rule chaining
- 🧪 Automated unit testing using xUnit and FluentAssertions
- 🔍 Easily extendable without modifying the engine core

---


---

## ▶️ How to Use

1. **Clone the repository**
2. **Open in Visual Studio or VS Code**
3. **Build the solution**
4. **Run the console app to see which rules pass or fail**
5. **Modify or add new rules without touching the core engine**
6. **Run tests with `dotnet test` to verify logic**

---

