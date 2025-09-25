using System.Text.RegularExpressions;

namespace Meteron.Domain.Customers;

public readonly record struct Email
{
    private static readonly Regex _regex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Email is required.", nameof(value));
        }

        if (!_regex.IsMatch(value))
        {
            throw new ArgumentException("Invalid email format.", nameof(value));
        }

        Value = value.Trim();
    }

    public override string ToString() => Value;
}