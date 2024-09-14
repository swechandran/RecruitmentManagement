$(document).ready(function () {
    // Regular expressions for validation
    const usernameRegex = /^[a-zA-Z0-9._]{5,20}$/; // Alphanumeric, underscores, 5-20 chars
    const passwordRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,20}$/; // One uppercase, one lowercase, one digit, 8-20 chars

    // On blur validation for Username
    $("#Username").on("blur", function () {
        const username = $(this).val();
        if (!usernameRegex.test(username)) {
            $(this).next(".text-danger").text("Invalid Username (5-20 chars, alphanumeric or underscores).");
        } else {
            $(this).next(".text-danger").text("");
        }
    });

    // On blur validation for Password
    $("#Password").on("blur", function () {
        const password = $(this).val();
        if (!passwordRegex.test(password)) {
            $(this).next(".text-danger").text("Password must be 8-20 chars with at least one digit, one uppercase, and one lowercase letter.");
        } else {
            $(this).next(".text-danger").text("");
        }
    });

    // Form submission validation
    $("form").submit(function (event) {
        let isValid = true;

        // Trigger blur events to show errors
        $("#Username").blur();
        $("#Password").blur();

        // Check if there are any errors displayed
        if ($(".text-danger").text() !== "") {
            isValid = false;
        }

        if (!isValid) {
            event.preventDefault(); // Prevent form submission if invalid
        }
    });
});
