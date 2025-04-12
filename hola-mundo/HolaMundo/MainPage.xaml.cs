using System.Text.RegularExpressions;

namespace HolaMundo;

public partial class MainPage : ContentPage
{
	private const string PasswordValidationMessage = "La contraseña no es válida. Debe contener al menos una letra mayúscula, una letra minúscula, un número y un carácter especial.";
	private const string PasswordMismatchMessage = "Las contraseñas no coinciden.";
	private const string PasswordValidMessage = "La contraseña ha sido validada";

	public MainPage()
	{
		InitializeComponent();
		btnValidate.IsEnabled = false; // Disable the button initially
	}

	/// Event handler for text changes in the password fields
	/// This method is called whenever the text in either password field changes
	private void OnTextChange (object sender, TextChangedEventArgs e)
	{
		var password1 = txtPass1?.Text ?? string.Empty;
		var password2 = txtPass2?.Text ?? string.Empty;

		// Enable the button only if both password fields are not empty
		btnValidate.IsEnabled = !string.IsNullOrWhiteSpace(password1) && !string.IsNullOrWhiteSpace(password2);
	}

	// Event handler for the Validate button click
	private async void OnValidateClick(object sender, EventArgs e)
	{
		var password1 = txtPass1?.Text ?? string.Empty;
        var password2 = txtPass2?.Text ?? string.Empty;

        var (success, message) = ValidatePasswords(password1, password2);
		var alertTitle = success ? "Success" : "Error";

		// Display an alert with the validation result
		// The alert will show a success message if the passwords are valid, or an error message otherwise
        await DisplayAlert(alertTitle, message, "Cerrar");

        SemanticScreenReader.Announce(message);
	}

	// Handle the password validatios and return message
	private (bool success, string message) ValidatePasswords(string password1, string password2)
    {
        if (!IsPasswordValid(password1))
            return (false, PasswordValidationMessage);

        if (password1 != password2)
            return (false, PasswordMismatchMessage);

        return (true, PasswordValidMessage);
    }

	// Validate password acceptance criteria
    private bool IsPasswordValid(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;
		
		// The regex patter checks for at least one uppercase letter, one lowercase letter, one digit, and one special character
        string pattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).+$";
        return Regex.IsMatch(password, pattern);
    }
}

