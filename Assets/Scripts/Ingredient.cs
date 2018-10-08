public class Ingredient
{
    // Nom de l'ingrédient
    public string Name;

    // Liste des effects sur le patient
    public Effect[] Effects;

    // L'ingrédient a t-il été écrasé
    public bool IsCrushed = false;

    // L'ingrédient est-il liquide
    public bool IsLiquid = false;

    // Multiplier les effets de l'ingrédient
    public void MultiplyEffects(int coefficient)
    {
        for (int i = 0; i < Effects.Length; ++i)
        {
            var effect = Effects[i];
            effect.ActionValue *= coefficient;
            Effects[i] = effect;
        }
    }
}
