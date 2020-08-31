using UnityEngine;

namespace MirkoZambito
{
    public class CoinManager : MonoBehaviour
    {
        private const string PPK_TOTALCOINS = "TOTALCOINS";
        [SerializeField] int initialCoins = 0;

        public int CollectedCoins { private set; get; }


        public int TotalCoins
        {
            private set
            {
                PlayerPrefs.SetInt(PPK_TOTALCOINS, value);
            }
            get
            {
                return PlayerPrefs.GetInt(PPK_TOTALCOINS, initialCoins);
            }
        }


        /// <summary>
        /// Add to total coins by given amountre
        /// </summary>
        /// <param name="amount"></param>
        public void AddTotalCoins(int amount)
        {
            TotalCoins += amount;
            PlayerPrefs.SetInt(PPK_TOTALCOINS, TotalCoins);
        }


        /// <summary>
        /// Remove from total coins by given amount
        /// </summary>
        /// <param name="amount"></param>
        public void RemoveTotalCoins(int amount)
        {
            TotalCoins -= amount;
            PlayerPrefs.SetInt(PPK_TOTALCOINS, TotalCoins);
        }


        /// <summary>
        /// Set the TotalCoins by the given amount.
        /// </summary>
        /// <param name="amount"></param>
        public void SetTotalCoins(int amount)
        {
            TotalCoins = amount;
            PlayerPrefs.SetInt(PPK_TOTALCOINS, TotalCoins);
        }


        /// <summary>
        /// Add collected coins by given amount
        /// </summary>
        /// <param name="amount"></param>
        public void AddCollectedCoins(int amount)
        {
            CollectedCoins += amount;
        }


        /// <summary>
        /// Remove from collected coins by given amount
        /// </summary>
        /// <param name="amount"></param>
        public void RemoveCollectedCoins(int amount)
        {
            CollectedCoins -= amount;
        }


        /// <summary>
        /// Set the CollectedCoins by the given amount.
        /// </summary>
        /// <param name="amount"></param>
        public void SetCollectedCoins(int amount)
        {
            CollectedCoins = amount;
        }

        /// <summary>
        /// Reset collected coins
        /// </summary>
        public void ResetCollectedCoins()
        {
            CollectedCoins = 0;
        }

    }
}
