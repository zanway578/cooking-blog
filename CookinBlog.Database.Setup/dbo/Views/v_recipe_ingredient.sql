

CREATE VIEW [dbo].[v_recipe_ingredient]
AS

SELECT g.RecipeID, g.Id AS RecipeGroupId, i.Name AS IngredientName, rgi.MeasurementAmount,
	rgi.MeasurementAmountNumerator, rgi.MeasurementAmountDenominator,
	m.Name AS MeasurementName, m.UnitName AS MeasurementUnitName
FROM recipe_group_ingredient AS rgi
JOIN recipe_group AS g
ON rgi.RecipeGroupId = g.Id
JOIN ingredient AS i
ON rgi.IngredientID = i.ID
JOIN measurement AS m
ON rgi.MeasurementID = m.ID

