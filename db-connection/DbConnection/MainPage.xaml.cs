using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using DbConnection.Services;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace DbConnection;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnConnectClicked(object sender, EventArgs e)
	{
		// Check if the user has entered all fields
		// and display an error message if any are missing
		var (success, error) = ValidateInputs();
		if (!success)
		{
			await DisplayAlert("Error", error, "OK");
			return;
		}

		// Create instance of the DbClient
		var dbClient = new DbClient(txtHost.Text, txtDbName.Text, txtUser.Text, txtPass.Text);

		// Connect to the database and get the tables
		(success, error, var tables) = await dbClient.GetTablesAsync();
		if (!success)
		{
			await DisplayAlert("Error", error, "OK");
			return;
		}

		if (tables.Count == 0)
		{
			await DisplayAlert("Warning", "No tables found in the database.", "OK");
			return;
		}

		// Create a StringBuilder to format the list of tables
		var sb = new StringBuilder("Tables in the database:").AppendLine();
		tables.ForEach(table => sb.AppendLine(table));

		await DisplayAlert("Success", sb.ToString(), "OK");

	}

	private (bool success, string message) ValidateInputs()
	{
		var requiredFields = new Dictionary<string, string>
		{
			{ "Host", txtHost.Text },
			{ "Database name", txtDbName.Text },
			{ "Username", txtUser.Text },
			{ "Password", txtPass.Text },
		};

		var message = new StringBuilder("The following fields are required:").AppendLine();
		var success = true;

		foreach (var field in requiredFields)
		{
			if (string.IsNullOrWhiteSpace(field.Value))
			{
				message.AppendLine($"- {field.Key}");
				success = false;
			}
		}

		return (success, message.ToString());
	}
}

