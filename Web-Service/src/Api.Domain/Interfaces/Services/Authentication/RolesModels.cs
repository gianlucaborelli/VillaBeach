public static class RolesModels
{
    public const string Admin = nameof(Admin);
    public const string Customer = nameof(Customer);

    private static readonly HashSet<string> AllRoles = new HashSet<string>
    {
        Admin,
        Customer
    };

    public static bool IsValidRole(string roleName)
    {
        return AllRoles.Contains(roleName);
    }
}