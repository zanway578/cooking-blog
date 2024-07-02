CREATE VIEW v_ingredient_nutrient
AS
SELECT itn.*, n.Name AS NutrientName
FROM ingredient_to_nutrient AS itn
JOIN nutrient AS n
ON itn.NutrientID = n.Id;