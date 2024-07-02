

/*  
	This select takes the conversion of nutrients to per 100g values and the conversion of ingredient amounts to grams
	and joins them together, making computing the amount of nutrients in the recipe easier
*/
CREATE VIEW [dbo].[v_recipe_ingredient_and_nutrient_normalized]
AS
SELECT
	r.ID as RecipeID,
	r.NumberServings,
	g.Id AS RecipeGroupID,
	ri.IngredientID,
	cn.NutrientID,
	cn.NutrientAmountPerHundredGrams,
	cn.NutrientMeasurement,
	crg.IngredientAmountGrams
FROM recipe AS r
JOIN recipe_group AS g
ON r.Id = g.RecipeID
JOIN recipe_group_ingredient AS ri
ON g.ID = ri.RecipeGroupID
JOIN v_converted_recipe_group_ingredient AS crg
ON crg.RecipeGroupID = g.ID AND crg.IngredientID = ri.IngredientID
LEFT JOIN ingredient_to_nutrient AS itn
ON itn.IngredientID = ri.IngredientID
LEFT JOIN v_ingredient_nutrient_per_hundred_grams AS cn
ON cn.IngredientID = itn.IngredientID AND cn.NutrientID = itn.NutrientID;

