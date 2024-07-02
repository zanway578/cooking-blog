


CREATE VIEW [dbo].[v_converted_recipe_group_ingredient]
AS
SELECT 
	RecipeGroupID,
	IngredientID,
	CASE
		-- can't convert from volumetric without this value
		WHEN IsVolumetric = 1 AND NumberGramsInACup IS NULL THEN NULL
		-- Have to convert to cups then cups to grams
		WHEN IsVolumetric = 1 THEN (ConversionRatio * MeasurementAmount) * NumberGramsInACup
		-- Direct conversion from weighted unit to grams
		ELSE ConversionRatio * MeasurementAmount
	END AS IngredientAmountGrams
FROM (
	SELECT rgi.IngredientID,
		rgi.RecipeGroupID,
		rgi.MeasurementAmount +
			(COALESCE(rgi.MeasurementAmountNumerator, 0.0) / COALESCE(rgi.MeasurementAmountDenominator, 1.0)) AS MeasurementAmount,
		m.IsVolumetric,
		i.NumberGramsInACup,
		m.ConversionRatio
	FROM recipe_group_ingredient AS rgi
	JOIN measurement AS m
	ON rgi.MeasurementID = m.ID
	JOIN ingredient AS i
	ON rgi.IngredientID = i.ID
) AS t

