# 🚀 Sign Up As a Company – Feature Documentation

## Overview
This document describes the **"Sign Up as a Company"** flow for anonymous users via the system portal.

---

## 📝 Sign Up Form

### 🧾 Form Title:
**"Sign Up as a Company"**

### 🧍‍♂️ Accessible by:
Anonymous (not logged-in) users.

### 📥 Form Fields:
| Field                  | Required | Validation                            | Notes                              |
|------------------------|----------|----------------------------------------|------------------------------------|
| Company Arabic Name    | ✅       | Not empty                              |                                    |
| Company English Name   | ✅       | Not empty                              |                                    |
| Email Address          | ✅       | Valid email format + not used before   | Unique in the system               |
| Phone Number           | ❌       | Valid phone number (optional)          |                                    |
| Website URL            | ❌       | Optional                                |                                    |
| Company Logo           | ❌       | Optional                                | Upload separately, preview allowed|

### 🔘 Buttons:
- **Sign Up** – Submits the form.

---

## 📤 After Sign-Up

1. ✅ If all validations pass:
   - An **OTP is sent** to the provided company email.
   - The user is redirected to an **OTP Validation Page**.
   - Tooltip displays the sent OTP for testing/demo purposes.

---

## 🔐 OTP Validation Page

- User enters the OTP received by email.
- If the OTP is valid → redirected to **Set Password Page**.

---

## 🔑 Set Password Page

### 🧾 Fields:
| Field             | Validation                                                                 |
|------------------|----------------------------------------------------------------------------|
| New Password      | - At least 6 characters <br> - One capital letter <br> - One special character <br> - One number |
| Confirm Password  | Must match the new password                                                |

✅ If validations pass:
- The password is saved for the company user.
- Redirect to the **Login Page**.

---

## 🔐 Login Page

- Company logs in using email and password.

---

## 🏠 Home Page (Post Login)

- Displays:
  - ✅ Company logo
  - ✅ Welcome message: `Hello {Company Name}`
  - ✅ Logout button

---

## 📌 Notes

- OTP is displayed in a **tooltip** (for test/demo purposes only).
- Logo upload is done **separately** with preview support before final submission.
- All user data must be validated on both **client-side and server-side**.
