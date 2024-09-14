$(document).ready(function () {
    // Regular expressions for validation
    const nameRegex = /^[A-Za-z\s]{2,30}$/; // For FirstName, LastName
    const phoneRegex = /^\d{10}$/; // For PhoneNumber (10 digits)
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/; // For EmailAddress
    const addressRegex = /^[A-Za-z0-9\s,.-]{5,100}$/; // For Address
    const usernameRegex = /^[a-zA-Z0-9._]{5,20}$/; // For Username (alphanumeric, underscores, 5-20 chars)
    const passwordRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,20}$/; // For Password (one uppercase, one lowercase, one digit, 8-20 chars)

    // On-blur validation for each field
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
        event.preventDefault(); // Prevent form submission
        // Trigger blur on all fields to validate before submit
        $("input, select").blur();

        // Check if there are any visible error messages
        if ($(".text-danger:visible").length === 0) {
            this.submit(); // Submit the form if no errors
        }
    });
});
