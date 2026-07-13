using Risk_n_Reward.Core.Models.SlotsModel.Symbols;

namespace Risk_n_Reward.Web.Models;

public class SlotToImage
{
    public static string ConvertToImage(SlotsSymbols slotsSymbol)
    {
        string slotName = slotsSymbol.ToString();
        
        return slotName switch
        {
            ("Cherry") => "~/images/gameassets/slots/cherries.png",
            ("ThreeCherries") => "~/images/gameassets/slots/three-cherries.png",
            ("Seven") => "~/images/gameassets/slots/seven.png",
            ("Clover") => "~/images/gameassets/slots/clover.png",
            ("Lemon") => "~/images/gameassets/slots/lemon.png",
            ("Bell") => "~/images/gameassets/slots/bell.png",
            ("Wild") => "~/images/gameassets/slots/wild.png",
            _ => "~/images/gameassets/errors/general/unknown.png"
        };
    }
}