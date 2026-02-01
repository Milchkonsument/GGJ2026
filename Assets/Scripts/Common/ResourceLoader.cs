using System.Linq;
using UnityEngine;

public static class ResourceLoader
{
    public static class Characters
    {
        public static readonly string DATA_PATH = "Data/Characters/";

        public static CharacterController CreateRandomCharacter()
        {
            var characterDataFiles = Resources.LoadAll<CharacterData>(DATA_PATH);
            var randomIndex = Random.Range(0, characterDataFiles.Length);
            var characterData = characterDataFiles[randomIndex];

            var characterGameObject = new GameObject(characterData.Name);
            var characterController = characterGameObject.AddComponent<CharacterController>();
            characterController.Data = characterData;
            characterController.CurrentMotivation = characterController.Data.BaseStats.BaseMotivation;

            return characterController;            
        }
    }

    public static class Candies
    {
        public static readonly string DATA_PATH = "Data/Candies/";

        public static CandyController CreateRandomCandy()
        {
            var candyFiles = Resources.LoadAll<CandyData>(DATA_PATH);
            var commonCandies = candyFiles.Where(c => c.Rarity.name == "Common").ToArray();
            var rareCandies = candyFiles.Where(c => c.Rarity.name == "Rare").ToArray();
            var epicCandies = candyFiles.Where(c => c.Rarity.name == "Epic").ToArray();
            var randomValue = Random.Range(0, 100);

            if (randomValue < 70)
            {
                var randomIndex = Random.Range(0, commonCandies.Length);
                return CreateCandyController(commonCandies[randomIndex]);
            }
            else if (randomValue < 90)
            {
                var randomIndex = Random.Range(0, rareCandies.Length);
                return CreateCandyController(rareCandies[randomIndex]);
            }
            else
            {
                var randomIndex = Random.Range(0, epicCandies.Length);
                return CreateCandyController(epicCandies[randomIndex]);
            }
        }

        private static CandyController CreateCandyController(CandyData candyData)
        {
            var candyGameObject = new GameObject(candyData.name);
            var candyController = candyGameObject.AddComponent<CandyController>();
            candyController.Data = candyData;
            return candyController;
        }
    }

    public static class Enemies
    {
        public static readonly string DATA_PATH = "Data/Enemies/";

        public static EnemyController CreateRandomEnemy()
        {
            var enemyDataFiles = Resources.LoadAll<EnemyData>(DATA_PATH);
            var randomIndex = Random.Range(0, enemyDataFiles.Length);
            var enemyData = enemyDataFiles[randomIndex];
            var enemySprite = enemyData.Sprite;

            var enemyGameObject = new GameObject(enemyData.Name);
            var enemyController = enemyGameObject.AddComponent<EnemyController>();
            enemyController.Data = enemyData;
            enemyController.CurrentMotivation = enemyController.Data.BaseStats.BaseMotivation;
            return enemyController;
        }
    }

    public static class Masks
    {
        public static readonly string DATA_PATH = "Data/Masks/";

        public static MaskController CreateRandomMask()
        {
            var maskDataFiles = Resources.LoadAll<MaskData>(DATA_PATH);
            var randomIndex = Random.Range(0, maskDataFiles.Length);
            var maskData = maskDataFiles[randomIndex];

            var maskGameObject = new GameObject(maskData.Name);
            var maskController = maskGameObject.AddComponent<MaskController>();
            maskController.Data = maskData;

            return maskController;
        }
    }

    public static class Houses
    {
        public static readonly string DATA_PATH = "Data/Houses/";

        public static HouseController CreateRandomHouse()
        {
            var houseDataFiles = Resources.LoadAll<HouseData>(DATA_PATH);
            var randomIndex = Random.Range(0, houseDataFiles.Length);
            var houseData = houseDataFiles[randomIndex];

            var houseGameObject = new GameObject(houseData.HouseName);
            var houseController = houseGameObject.AddComponent<HouseController>();
            houseController.Data = houseData;

            return houseController;
        }
    }
}