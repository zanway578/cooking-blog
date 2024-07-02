




CREATE VIEW [dbo].[v_ingredient_nutrient_per_hundred_grams]
AS
SELECT
	itn.IngredientID,
	itn.NutrientID,
	-- convert amount to be relative to a hundred grams
	itn.NutrientAmount * (calorie.NutrientAmount / i.CaloriesInOneHundredGrams) AS NutrientAmountPerHundredGrams,
	itn.NutrientMeasurement
FROM ingredient AS i
LEFT JOIN ingredient_to_nutrient AS itn
ON i.ID = itn.IngredientID
LEFT JOIN nutrient AS n
ON itn.NutrientID = n.ID
-- Get KCAL amount and map to ingredient to allow for conversion to per 100 grams
LEFT JOIN ingredient_to_nutrient AS calorie
ON i.ID = calorie.IngredientID AND calorie.NutrientMeasurement = 'KCAL'
