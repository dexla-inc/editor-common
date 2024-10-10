using System.Text.RegularExpressions;
using Dexla.Common.Editor.Responses;

namespace Dexla.Common.Editor.Models;

public static class ColorShades
{
    /// <summary>
    /// Sets a new color for the specified type by removing all existing shades
    /// and adding the new color to the .6 shade.
    /// </summary>
    /// <param name="shades">The list of ColorShadeDto to operate on.</param>
    /// <param name="type">The color type (e.g., "Primary", "Secondary").</param>
    /// <param name="newHex">The new hex color code (e.g., "#FF5733").</param>
    /// <param name="isDefault">Is this a default color or new</param>
    /// <exception cref="ArgumentException">Thrown when the type is invalid or hex format is incorrect.</exception>
    public static void SetColorShade(this List<ColorShadeDto> shades, string type, string newHex, bool isDefault = true)
    {
        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentException("Color type cannot be null or empty.", nameof(type));

        if (string.IsNullOrWhiteSpace(newHex))
            throw new ArgumentException("Hex value cannot be null or empty.", nameof(newHex));

        // Validate hex format
        if (!IsValidHex(newHex))
            throw new ArgumentException("Invalid hex color format.", nameof(newHex));

        // Define the prefix to identify the color type
        string prefix = $"{type}.";

        // Remove all existing shades for the specified type
        shades.RemoveAll(c => c.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));

        // Create the new ColorShadeDto for the .6 shade
        ColorShadeDto newColorShade = new()
        {
            Hex = newHex,
            Name = $"{type}.6",
            IsDefault = isDefault, 
            FriendlyName = $"{type}" 
        };

        // Add the new shade
        shades.Add(newColorShade);

        // Sort the list to maintain order
        SortColors(shades);
    }

    /// <summary>
    /// Validates if the provided string is a valid hex color code.
    /// Supports both 3 and 6 character hex codes.
    /// </summary>
    /// <param name="hex">The hex color code to validate.</param>
    /// <returns>True if valid; otherwise, false.</returns>
    private static bool IsValidHex(string hex)
    {
        return !string.IsNullOrWhiteSpace(hex) &&
               Regex.IsMatch(hex, "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$");
    }

    /// <summary>
    /// Sorts the Colors list based on the predefined DesiredColorOrder.
    /// Colors not in the DesiredColorOrder are placed at the end in alphabetical order.
    /// </summary>
    /// <param name="colors">The list of ColorShadeDto to sort.</param>
    private static void SortColors(List<ColorShadeDto> colors)
    {
        // Create a dictionary for quick lookup of desired order indices
        // No changes needed here
        Dictionary<string, int> colorOrderDict = DesiredColorOrder
            .Select((name, index) => new { name, index })
            .ToDictionary(x => x.name, x => x.index, StringComparer.OrdinalIgnoreCase); // Added case-insensitive comparer

        colors.Sort((x, y) =>
        {
            // Extract base names by removing any suffix after a dot
            string xBaseName = GetBaseName(x.Name);
            string yBaseName = GetBaseName(y.Name);

            bool xInOrder = colorOrderDict.TryGetValue(xBaseName, out int xIndex);
            bool yInOrder = colorOrderDict.TryGetValue(yBaseName, out int yIndex);

            return xInOrder switch
            {
                true when yInOrder => xIndex.CompareTo(yIndex),
                true => -1, // x comes before y
                _ => yInOrder
                    ? 1
                    : // If neither is in the desired order, sort alphabetically
                    string.Compare(xBaseName, yBaseName, StringComparison.OrdinalIgnoreCase)
            };
        });
    }

    /// <summary>
    /// Extracts the base name by removing any suffix after the first dot.
    /// For example, "Primary.6" becomes "Primary".
    /// </summary>
    /// <param name="name">The original name.</param>
    /// <returns>The base name without suffix.</returns>
    private static string GetBaseName(string name)
    {
        if (string.IsNullOrEmpty(name))
            return name;

        int dotIndex = name.IndexOf('.');
        return dotIndex > 0 ? name.Substring(0, dotIndex) : name;
    }
    
    private static readonly List<string> DesiredColorOrder =
    [
        "Primary",
        "PrimaryText",
        "Secondary",
        "SecondaryText",
        "Tertiary",
        "TertiaryText",
        "Background",
        "Danger",
        "Warning",
        "Success",
        "Neutral",
        "Black",
        "White",
        "Border"
    ];
    
    public static readonly List<ColorShadeDto> DefaultShades =
    [
        new() { Hex = "#EAF0FA", Name = "Primary.0", IsDefault = true, FriendlyName = "Primary 0" },
        new() { Hex = "#D5E0F5", Name = "Primary.1", IsDefault = true, FriendlyName = "Primary 1" },
        new() { Hex = "#C1D1EF", Name = "Primary.2", IsDefault = true, FriendlyName = "Primary 2" },
        new() { Hex = "#ACD1EA", Name = "Primary.3", IsDefault = true, FriendlyName = "Primary 3" },
        new() { Hex = "#97B2E5", Name = "Primary.4", IsDefault = true, FriendlyName = "Primary 4" },
        new() { Hex = "#82A3E0", Name = "Primary.5", IsDefault = true, FriendlyName = "Primary 5" },
        new() { Hex = "#2F65CB", Name = "Primary.6", IsDefault = true, FriendlyName = "Primary" },
        new() { Hex = "#2A5BB7", Name = "Primary.7", IsDefault = true, FriendlyName = "Primary 7" },
        new() { Hex = "#2651A2", Name = "Primary.8", IsDefault = true, FriendlyName = "Primary 8" },
        new() { Hex = "#21478E", Name = "Primary.9", IsDefault = true, FriendlyName = "Primary 9" },

        new() { Hex = "#FFFFFF", Name = "PrimaryText.0", IsDefault = true, FriendlyName = "Primary Text 0" },
        new() { Hex = "#FFFFFF", Name = "PrimaryText.1", IsDefault = true, FriendlyName = "Primary Text 1" },
        new() { Hex = "#FFFFFF", Name = "PrimaryText.2", IsDefault = true, FriendlyName = "Primary Text 2" },
        new() { Hex = "#FFFFFF", Name = "PrimaryText.3", IsDefault = true, FriendlyName = "Primary Text 3" },
        new() { Hex = "#FFFFFF", Name = "PrimaryText.4", IsDefault = true, FriendlyName = "Primary Text 4" },
        new() { Hex = "#FFFFFF", Name = "PrimaryText.5", IsDefault = true, FriendlyName = "Primary Text 5" },
        new() { Hex = "#FFFFFF", Name = "PrimaryText.6", IsDefault = true, FriendlyName = "Primary Text" },
        new() { Hex = "#F5F8F8", Name = "PrimaryText.7", IsDefault = true, FriendlyName = "Primary Text 7" },
        new() { Hex = "#CCCCCC", Name = "PrimaryText.8", IsDefault = true, FriendlyName = "Primary Text 8" },
        new() { Hex = "#B3B3B3", Name = "PrimaryText.9", IsDefault = true, FriendlyName = "Primary Text 9" },

        new() { Hex = "#FBFBFB", Name = "Secondary.0", IsDefault = true, FriendlyName = "Secondary 0" },
        new() { Hex = "#F7F7F7", Name = "Secondary.1", IsDefault = true, FriendlyName = "Secondary 1" },
        new() { Hex = "#F4F4F4", Name = "Secondary.2", IsDefault = true, FriendlyName = "Secondary 2" },
        new() { Hex = "#F0F0F0", Name = "Secondary.3", IsDefault = true, FriendlyName = "Secondary 3" },
        new() { Hex = "#ECECEC", Name = "Secondary.4", IsDefault = true, FriendlyName = "Secondary 4" },
        new() { Hex = "#E8E8E8", Name = "Secondary.5", IsDefault = true, FriendlyName = "Secondary 5" },
        new() { Hex = "#D9D9D9", Name = "Secondary.6", IsDefault = true, FriendlyName = "Secondary" },
        new() { Hex = "#C3C3C3", Name = "Secondary.7", IsDefault = true, FriendlyName = "Secondary 7" },
        new() { Hex = "#AEAEAE", Name = "Secondary.8", IsDefault = true, FriendlyName = "Secondary 8" },
        new() { Hex = "#989898", Name = "Secondary.9", IsDefault = true, FriendlyName = "Secondary 9" },

        new() { Hex = "#E6E6E6", Name = "SecondaryText.0", IsDefault = true, FriendlyName = "Secondary Text 0" },
        new() { Hex = "#CCCCCC", Name = "SecondaryText.1", IsDefault = true, FriendlyName = "Secondary Text 1" },
        new() { Hex = "#B3B3B3", Name = "SecondaryText.2", IsDefault = true, FriendlyName = "Secondary Text 2" },
        new() { Hex = "#999999", Name = "SecondaryText.3", IsDefault = true, FriendlyName = "Secondary Text 3" },
        new() { Hex = "#808080", Name = "SecondaryText.4", IsDefault = true, FriendlyName = "Secondary Text 4" },
        new() { Hex = "#323232", Name = "SecondaryText.5", IsDefault = true, FriendlyName = "Secondary Text 5" },
        new() { Hex = "#000000", Name = "SecondaryText.6", IsDefault = true, FriendlyName = "Secondary Text" },
        new() { Hex = "#000000", Name = "SecondaryText.7", IsDefault = true, FriendlyName = "Secondary Text 7" },
        new() { Hex = "#000000", Name = "SecondaryText.8", IsDefault = true, FriendlyName = "Secondary Text 8" },
        new() { Hex = "#000000", Name = "SecondaryText.9", IsDefault = true, FriendlyName = "Secondary Text 9" },

        new() { Hex = "#FCF2ED", Name = "Tertiary.0", IsDefault = true, FriendlyName = "Tertiary 0" },
        new() { Hex = "#FAE5DC", Name = "Tertiary.1", IsDefault = true, FriendlyName = "Tertiary 1" },
        new() { Hex = "#F7D9CA", Name = "Tertiary.2", IsDefault = true, FriendlyName = "Tertiary 2" },
        new() { Hex = "#F5CCB9", Name = "Tertiary.3", IsDefault = true, FriendlyName = "Tertiary 3" },
        new() { Hex = "#F2BFA7", Name = "Tertiary.4", IsDefault = true, FriendlyName = "Tertiary 4" },
        new() { Hex = "#EFB295", Name = "Tertiary.5", IsDefault = true, FriendlyName = "Tertiary 5" },
        new() { Hex = "#E57F4F", Name = "Tertiary.6", IsDefault = true, FriendlyName = "Tertiary" },
        new() { Hex = "#CE7247", Name = "Tertiary.7", IsDefault = true, FriendlyName = "Tertiary 7" },
        new() { Hex = "#B7663F", Name = "Tertiary.8", IsDefault = true, FriendlyName = "Tertiary 8" },
        new() { Hex = "#A05937", Name = "Tertiary.9", IsDefault = true, FriendlyName = "Tertiary 9" },

        new() { Hex = "#FFFFFF", Name = "TertiaryText.0", IsDefault = true, FriendlyName = "Tertiary Text 0" },
        new() { Hex = "#FFFFFF", Name = "TertiaryText.1", IsDefault = true, FriendlyName = "Tertiary Text 1" },
        new() { Hex = "#FFFFFF", Name = "TertiaryText.2", IsDefault = true, FriendlyName = "Tertiary Text 2" },
        new() { Hex = "#FFFFFF", Name = "TertiaryText.3", IsDefault = true, FriendlyName = "Tertiary Text 3" },
        new() { Hex = "#FFFFFF", Name = "TertiaryText.4", IsDefault = true, FriendlyName = "Tertiary Text 4" },
        new() { Hex = "#FFFFFF", Name = "TertiaryText.5", IsDefault = true, FriendlyName = "Tertiary Text 5" },
        new() { Hex = "#FFFFFF", Name = "TertiaryText.6", IsDefault = true, FriendlyName = "Tertiary Text" },
        new() { Hex = "#F5F8F8", Name = "TertiaryText.7", IsDefault = true, FriendlyName = "Tertiary Text 7" },
        new() { Hex = "#CCCCCC", Name = "TertiaryText.8", IsDefault = true, FriendlyName = "Tertiary Text 8" },
        new() { Hex = "#B3B3B3", Name = "TertiaryText.9", IsDefault = true, FriendlyName = "Tertiary Text 9" },

        new() { Hex = "#FFFFFF", Name = "Background.0", IsDefault = true, FriendlyName = "Background 0" },
        new() { Hex = "#FFFFFF", Name = "Background.1", IsDefault = true, FriendlyName = "Background 1" },
        new() { Hex = "#FFFFFF", Name = "Background.2", IsDefault = true, FriendlyName = "Background 2" },
        new() { Hex = "#FFFFFF", Name = "Background.3", IsDefault = true, FriendlyName = "Background 3" },
        new() { Hex = "#FFFFFF", Name = "Background.4", IsDefault = true, FriendlyName = "Background 4" },
        new() { Hex = "#FFFFFF", Name = "Background.5", IsDefault = true, FriendlyName = "Background 5" },
        new() { Hex = "#FFFFFF", Name = "Background.6", IsDefault = true, FriendlyName = "Background" },
        new() { Hex = "#F5F8F8", Name = "Background.7", IsDefault = true, FriendlyName = "Background 7" },
        new() { Hex = "#CCCCCC", Name = "Background.8", IsDefault = true, FriendlyName = "Background 8" },
        new() { Hex = "#B3B3B3", Name = "Background.9", IsDefault = true, FriendlyName = "Background 9" },

        new() { Hex = "#FFE8E8", Name = "Danger.0", IsDefault = true, FriendlyName = "Danger 0" },
        new() { Hex = "#FFD1D2", Name = "Danger.1", IsDefault = true, FriendlyName = "Danger 1" },
        new() { Hex = "#FFBABB", Name = "Danger.2", IsDefault = true, FriendlyName = "Danger 2" },
        new() { Hex = "#FFA3A4", Name = "Danger.3", IsDefault = true, FriendlyName = "Danger 3" },
        new() { Hex = "#FF8C8E", Name = "Danger.4", IsDefault = true, FriendlyName = "Danger 4" },
        new() { Hex = "#FE7577", Name = "Danger.5", IsDefault = true, FriendlyName = "Danger 5" },
        new() { Hex = "#FE191C", Name = "Danger.6", IsDefault = true, FriendlyName = "Danger" },
        new() { Hex = "#E51719", Name = "Danger.7", IsDefault = true, FriendlyName = "Danger 7" },
        new() { Hex = "#CB1416", Name = "Danger.8", IsDefault = true, FriendlyName = "Danger 8" },
        new() { Hex = "#B21212", Name = "Danger.9", IsDefault = true, FriendlyName = "Danger 9" },

        new() { Hex = "#FFFAE6", Name = "Warning.0", IsDefault = true, FriendlyName = "Warning 0" },
        new() { Hex = "#FFF5CC", Name = "Warning.1", IsDefault = true, FriendlyName = "Warning 1" },
        new() { Hex = "#FFF0B3", Name = "Warning.2", IsDefault = true, FriendlyName = "Warning 2" },
        new() { Hex = "#FFEB99", Name = "Warning.3", IsDefault = true, FriendlyName = "Warning 3" },
        new() { Hex = "#FFE680", Name = "Warning.4", IsDefault = true, FriendlyName = "Warning 4" },
        new() { Hex = "#FFE066", Name = "Warning.5", IsDefault = true, FriendlyName = "Warning 5" },
        new() { Hex = "#FFCC00", Name = "Warning.6", IsDefault = true, FriendlyName = "Warning" },
        new() { Hex = "#E6B800", Name = "Warning.7", IsDefault = true, FriendlyName = "Warning 7" },
        new() { Hex = "#CC9900", Name = "Warning.8", IsDefault = true, FriendlyName = "Warning 8" },
        new() { Hex = "#B38B00", Name = "Warning.9", IsDefault = true, FriendlyName = "Warning 9" },

        new() { Hex = "#E7FBF4", Name = "Success.0", IsDefault = true, FriendlyName = "Success 0" },
        new() { Hex = "#CFF6E8", Name = "Success.1", IsDefault = true, FriendlyName = "Success 1" },
        new() { Hex = "#B7F2DD", Name = "Success.2", IsDefault = true, FriendlyName = "Success 2" },
        new() { Hex = "#9FEDD2", Name = "Success.3", IsDefault = true, FriendlyName = "Success 3" },
        new() { Hex = "#88EAC7", Name = "Success.4", IsDefault = true, FriendlyName = "Success 4" },
        new() { Hex = "#70E5BB", Name = "Success.5", IsDefault = true, FriendlyName = "Success 5" },
        new() { Hex = "#10D48E", Name = "Success.6", IsDefault = true, FriendlyName = "Success" },
        new() { Hex = "#0EBF80", Name = "Success.7", IsDefault = true, FriendlyName = "Success 7" },
        new() { Hex = "#0AAA73", Name = "Success.8", IsDefault = true, FriendlyName = "Success 8" },
        new() { Hex = "#099361", Name = "Success.9", IsDefault = true, FriendlyName = "Success 9" },

        new() { Hex = "#FEFEFE", Name = "Neutral.0", IsDefault = true, FriendlyName = "Neutral 0" },
        new() { Hex = "#FCFCFC", Name = "Neutral.1", IsDefault = true, FriendlyName = "Neutral 1" },
        new() { Hex = "#FBFBFB", Name = "Neutral.2", IsDefault = true, FriendlyName = "Neutral 2" },
        new() { Hex = "#F9F9F9", Name = "Neutral.3", IsDefault = true, FriendlyName = "Neutral 3" },
        new() { Hex = "#F8F8F8", Name = "Neutral.4", IsDefault = true, FriendlyName = "Neutral 4" },
        new() { Hex = "#F7F7F7", Name = "Neutral.5", IsDefault = true, FriendlyName = "Neutral 5" },
        new() { Hex = "#F1F1F1", Name = "Neutral.6", IsDefault = true, FriendlyName = "Neutral" },
        new() { Hex = "#D9D9D9", Name = "Neutral.7", IsDefault = true, FriendlyName = "Neutral 7" },
        new() { Hex = "#C1C1C1", Name = "Neutral.8", IsDefault = true, FriendlyName = "Neutral 8" },
        new() { Hex = "#A9A9A9", Name = "Neutral.9", IsDefault = true, FriendlyName = "Neutral 9" },

        new() { Hex = "#E6E6E6", Name = "Black.0", IsDefault = true, FriendlyName = "Black 0" },
        new() { Hex = "#CCCCCC", Name = "Black.1", IsDefault = true, FriendlyName = "Black 1" },
        new() { Hex = "#B3B3B3", Name = "Black.2", IsDefault = true, FriendlyName = "Black 2" },
        new() { Hex = "#999999", Name = "Black.3", IsDefault = true, FriendlyName = "Black 3" },
        new() { Hex = "#808080", Name = "Black.4", IsDefault = true, FriendlyName = "Black 4" },
        new() { Hex = "#323232", Name = "Black.5", IsDefault = true, FriendlyName = "Black 5" },
        new() { Hex = "#000000", Name = "Black.6", IsDefault = true, FriendlyName = "Black" },
        new() { Hex = "#000000", Name = "Black.7", IsDefault = true, FriendlyName = "Black 7" },
        new() { Hex = "#000000", Name = "Black.8", IsDefault = true, FriendlyName = "Black 8" },
        new() { Hex = "#000000", Name = "Black.9", IsDefault = true, FriendlyName = "Black 9" },

        new() { Hex = "#FFFFFF", Name = "White.0", IsDefault = true, FriendlyName = "White 0" },
        new() { Hex = "#FFFFFF", Name = "White.1", IsDefault = true, FriendlyName = "White 1" },
        new() { Hex = "#FFFFFF", Name = "White.2", IsDefault = true, FriendlyName = "White 2" },
        new() { Hex = "#FFFFFF", Name = "White.3", IsDefault = true, FriendlyName = "White 3" },
        new() { Hex = "#FFFFFF", Name = "White.4", IsDefault = true, FriendlyName = "White 4" },
        new() { Hex = "#FFFFFF", Name = "White.5", IsDefault = true, FriendlyName = "White 5" },
        new() { Hex = "#FFFFFF", Name = "White.6", IsDefault = true, FriendlyName = "White" },
        new() { Hex = "#F5F8F8", Name = "White.7", IsDefault = true, FriendlyName = "White 7" },
        new() { Hex = "#CCCCCC", Name = "White.8", IsDefault = true, FriendlyName = "White 8" },
        new() { Hex = "#B3B3B3", Name = "White.9", IsDefault = true, FriendlyName = "White 9" },

        new() { Hex = "#FDFDFD", Name = "Border.0", IsDefault = true, FriendlyName = "Border 0" },
        new() { Hex = "#FCFCFC", Name = "Border.1", IsDefault = true, FriendlyName = "Border 1" },
        new() { Hex = "#FAFAFA", Name = "Border.2", IsDefault = true, FriendlyName = "Border 2" },
        new() { Hex = "#F8F8F8", Name = "Border.3", IsDefault = true, FriendlyName = "Border 3" },
        new() { Hex = "#F7F7F7", Name = "Border.4", IsDefault = true, FriendlyName = "Border 4" },
        new() { Hex = "#F5F5F5", Name = "Border.5", IsDefault = true, FriendlyName = "Border 5" },
        new() { Hex = "#EEEEEE", Name = "Border.6", IsDefault = true, FriendlyName = "Border" },
        new() { Hex = "#D6D6D6", Name = "Border.7", IsDefault = true, FriendlyName = "Border 7" },
        new() { Hex = "#BEBEBE", Name = "Border.8", IsDefault = true, FriendlyName = "Border 8" },
        new() { Hex = "#A7A7A7", Name = "Border.9", IsDefault = true, FriendlyName = "Border 9" }
    ];

    public static readonly List<ColorDto> DefaultColors =
    [
        new() { Hex = "#2F65CB", Name = "Primary", IsDefault = true, FriendlyName = "Primary", },
        new() { Hex = "#FFFFFF", Name = "PrimaryText", IsDefault = true, FriendlyName = "Primary Text", },
        new() { Hex = "#D9D9D9", Name = "Secondary", IsDefault = true, FriendlyName = "Secondary" },
        new() { Hex = "#000000", Name = "SecondaryText", IsDefault = true, FriendlyName = "Secondary Text" },
        new() { Hex = "#E57F4F", Name = "Tertiary", IsDefault = true, FriendlyName = "Tertiary" },
        new() { Hex = "#FFFFFF", Name = "TertiaryText", IsDefault = true, FriendlyName = "Tertiary Text" },
        new() { Hex = "#FFFFFF", Name = "Background", IsDefault = true, FriendlyName = "Background" },
        new() { Hex = "#FE191C", Name = "Danger", IsDefault = true, FriendlyName = "Danger" },
        new() { Hex = "#FFCC00", Name = "Warning", IsDefault = true, FriendlyName = "Warning" },
        new() { Hex = "#10D48E", Name = "Success", IsDefault = true, FriendlyName = "Success" },
        new() { Hex = "#F1F1F1", Name = "Neutral", IsDefault = true, FriendlyName = "Neutral" },
        new() { Hex = "#000000", Name = "Black", IsDefault = true, FriendlyName = "Black" },
        new() { Hex = "#FFFFFF", Name = "White", IsDefault = true, FriendlyName = "White" },
        new() { Hex = "#EEEEEE", Name = "Border", IsDefault = true, FriendlyName = "Border" }
    ];
}