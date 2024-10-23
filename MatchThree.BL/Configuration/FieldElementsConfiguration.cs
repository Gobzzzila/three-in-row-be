using MatchThree.Domain.Configuration;
using MatchThree.Shared.Enums;

namespace MatchThree.BL.Configuration;

public static class FieldElementsConfiguration
{
    private static readonly Dictionary<CryptoTypes, Dictionary<ElementLevels, FieldElementParameters>> FieldParams;

    static FieldElementsConfiguration()
    {
        FieldParams = new Dictionary<CryptoTypes, Dictionary<ElementLevels, FieldElementParameters>>
        {
            {
                CryptoTypes.Ton, new Dictionary<ElementLevels, FieldElementParameters>
                {
                    {
                        ElementLevels.Level1, new FieldElementParameters
                        {

                        }
                    }
                }
            },
            {
                CryptoTypes.Ston, new Dictionary<ElementLevels, FieldElementParameters>
                {
                    {
                        ElementLevels.Level1, new FieldElementParameters
                        {
                            
                        }
                    }
                }
            }
        };
    }
}