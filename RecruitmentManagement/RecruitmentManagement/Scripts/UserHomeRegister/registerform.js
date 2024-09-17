$(document).ready(function () {
    
    const nameRegex = /^[A-Za-z\s]{2,30}$/; 
    const phoneRegex = /^\d{10}$/; 
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/; 
    const addressRegex = /^[A-Za-z0-9\s,.-]{5,100}$/; 
    const usernameRegex = /^[a-zA-Z0-9._]{5,20}$/; 
    const passwordRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,20}$/; 

    // On-blur validation 
    $("#FirstName").blur(function () {
        const firstName = $(this).val();
        if (!nameRegex.test(firstName)) {
            $(this).next(".text-danger").text("Invalid First Name (only letters, 2-30 chars).");
        } else {
            $(this).next(".text-danger").text("");
        }
    });

    $("#LastName").blur(function () {
        const lastName = $(this).val();
        if (!nameRegex.test(lastName)) {
            $(this).next(".text-danger").text("Invalid Last Name (only letters, 2-30 chars).");
        } else {
            $(this).next(".text-danger").text("");
        }
    });

    $("#DateOfBirth").blur(function () {
        const dob = $(this).val();
        if (!dob) {
            $(this).next(".text-danger").text("Date of Birth is required.");
        } else {
            $(this).next(".text-danger").text("");
        }
    });

    $("#Gender").blur(function () {
        const gender = $(this).val();
        if (gender === "Select Gender") {
            $(this).next(".text-danger").text("Please select your gender.");
        } else {
            $(this).next(".text-danger").text("");
        }
    });

    $("#PhoneNumber").blur(function () {
        const phoneNumber = $(this).val();
        if (!phoneRegex.test(phoneNumber)) {
            $(this).next(".text-danger").text("Invalid Phone Number (10 digits required).");
        } else {
            $(this).next(".text-danger").text("");
        }
    });

    $("#EmailAddress").blur(function () {
        const emailAddress = $(this).val();
        if (!emailRegex.test(emailAddress)) {
            $(this).next(".text-danger").text("Invalid Email Address format.");
        } else {
            $(this).next(".text-danger").text("");
        }
    });

    $("#Address").blur(function () {
        const address = $(this).val();
        if (!addressRegex.test(address)) {
            $(this).next(".text-danger").text("Invalid Address (5-100 chars).");
        } else {
            $(this).next(".text-danger").text("");
        }
    });

    $("#state").blur(function () {
        const state = $(this).val();
        if (state === "Select State") {
            $(this).next(".text-danger").text("Please select a state.");
        } else {
            $(this).next(".text-danger").text("");
        }
    });

    $("#city").blur(function () {
        const city = $(this).val();
        if (!city) {
            $(this).next(".text-danger").text("Please select a city.");
        } else {
            $(this).next(".text-danger").text("");
        }
    });

    $("#Username").blur(function () {
        const username = $(this).val();
        if (!usernameRegex.test(username)) {
            $(this).next(".text-danger").text("Invalid Username (5-20 chars, alphanumeric or underscores).");
        } else {
            $(this).next(".text-danger").text("");
        }
    });

    $("#Password").blur(function () {
        const password = $(this).val();
        if (!passwordRegex.test(password)) {
            $(this).next(".text-danger").text("Password must be 8-20 chars with at least one digit, one uppercase, and one lowercase letter.");
        } else {
            $(this).next(".text-danger").text("");
        }
    });

    $("#ConfirmPassword").blur(function () {
        const confirmPassword = $(this).val();
        const password = $("#Password").val();
        if (password !== confirmPassword) {
            $(this).next(".text-danger").text("Passwords do not match.");
        } else {
            $(this).next(".text-danger").text("");
        }
    });

    // On form submission
    $("form").submit(function (event) {
        
        
        $("input, select").blur();
        if ($(".text-danger:visible").length === 0) {
            this.submit(); 
        }
    });
});
