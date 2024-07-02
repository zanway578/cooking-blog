



CREATE VIEW [dbo].[v_recipe_nutrition_value]
AS
SELECT
	RecipeID,
	NutrientID,
	n.Name AS NutrientName,
	NutrientMeasurement, 
	Amount / r.NumberServings AS Amount,
	-- NotComputed is number of null nutrients found when pulling sums
	CAST (
		CASE
			WHEN NotComputed > 0 THEN 1
			ELSE 0
		END AS BIT
	) AS ComputationFailed
FROM (
	SELECT 
	RecipeID,
	NutrientID,
	NutrientMeasurement,
	SUM(NutrientAmountPerHundredGrams * (IngredientAmountGrams / 100)) AS Amount,
	-- null nutrient implies ingredient doesn't have computed nutrient info and so we mark as failure
	SUM(
		CASE
			WHEN NutrientID IS NULL THEN 1
			ELSE 0
		END
	) AS NotComputed
	FROM v_recipe_ingredient_and_nutrient_normalized
	GROUP BY RecipeID, NutrientID, NutrientMeasurement
) As t
JOIN nutrient AS n
ON t.NutrientID = n.ID
JOIN recipe AS r
ON r.ID = t.RecipeID
