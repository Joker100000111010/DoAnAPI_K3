// auth.js

document.addEventListener("DOMContentLoaded", function () {
  const loginForm = document.getElementById("login-form");
  const registerForm = document.getElementById("register-form");

  if (loginForm) {
      loginForm.addEventListener("submit", function (e) {
          e.preventDefault();

          const email = document.getElementById("email").value;
          const password = document.getElementById("password").value;

          const user = JSON.parse(localStorage.getItem(email));

          if (user && user.password === password) {
              alert("Login successful!");
              // Redirect or perform other actions upon successful login
          } else {
              alert("Invalid email or password.");
          }
      });
  }

  if (registerForm) {
      registerForm.addEventListener("submit", function (e) {
          e.preventDefault();

          const email = document.getElementById("email").value;
          const password = document.getElementById("password").value;
          const confirmPassword = document.getElementById("confirm-password").value;

          if (password !== confirmPassword) {
              alert("Passwords do not match.");
              return;
          }

          const user = {
              email: email,
              password: password
          };

          localStorage.setItem(email, JSON.stringify(user));
          alert("Registration successful!");
          // Redirect or perform other actions upon successful registration
      });
  }
});
