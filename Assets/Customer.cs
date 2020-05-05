using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{

    public Ingredient[] collectiblesInScene;
    public Inventory inventory;
    public int score = 0;
    public TextMesh scoreSummary;
    public TextMesh itemSummary;
    public TextMesh prompt;
    public GameObject playerCamera;
    public RaycastHit outHit;
    public Ingredient collectedItem;
    public int numOfItems = 0;
    int kiwiCount = 0;
    int strawberryCount = 0;

                        // foreach (KeyValuePair<collectible, int> item in inventory.itemsCollected) 
                        // {
                        //     itemSummary.text += "\n # of " + item.Key.name + ": " + item.Value + ", Item Value: " + item.Key.points; 
                        // }
    void Update()
    {
        
        if (Input.GetKeyDown("q")) {
            prompt.text = "";
            //itemSummary.text += "\n # of " + item.Key.name + ": " + item.Value + ", Item Value: " + item.Key.points;
        }
        int layerMask = 1 << 10;
        layerMask = ~layerMask;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out outHit, 100.0f, layerMask))
            {             
                if (Input.GetKeyDown("e")) 
                {                  
                    Debug.Log("Pressed e");  
                    collectedItem = outHit.collider.gameObject.GetComponent<Ingredient>();
                    if ((collectedItem.name == "Coconut")||(collectedItem.name == "KiwiSlice")||(collectedItem.name == "Banana")||(collectedItem.name == "Strawberry")||(collectedItem.name == "Acai")||(collectedItem.name == "Honey")) 
                    {   
                        GameObject objSelected = (GameObject)Resources.Load(GetGameObjectPath(collectedItem), typeof(GameObject));
                        Ingredient currentCollectible = objSelected.gameObject.GetComponent<Ingredient>();
                        if (inventory.itemsCollected.ContainsKey(currentCollectible)) 
                        {
                            inventory.itemsCollected[currentCollectible]++;
                        } 
                        else 
                        {
                            inventory.itemsCollected.Add(currentCollectible, 1);
                        }
                        score = score + currentCollectible.points;
                        numOfItems++;

                    }                        
                    Destroy(outHit.collider.gameObject); 
                    
                    if (collectedItem.name == "Banana") 
                    {
                        prompt.text = "Congratulations!" + "\nYou found the banana, our most popular Acai Bowl topping.";
                    }
                    if (collectedItem.name == "Honey") 
                    {
                        prompt.text = "Congratulations!" + "\nYou found honey, which we collect only from local honey suppliers.";
                    }
                    if (collectedItem.name == "Strawberry") 
                    {
                        strawberryCount++;
                        if (strawberryCount == 3)
                        {
                            prompt.text = "Congratulations!" + "\nYou found all the Strawberries, which are locally grown.";
                        }
                    }
                    if (collectedItem.name == "KiwiSlice") 
                    {
                        kiwiCount++;
                        if (kiwiCount == 4)
                        {
                            prompt.text = "Congratulations!" + "\nYou found all the kiwi slices, our second most popular topping.";
                        }
                    }
                    if (collectedItem.name == "Acai") 
                    {
                        prompt.text = "Congratulations!" + "\nYou found the Acai Base, made from Acai berries, a SuperFood \ngrown in the Amazon, packed with antioxidant properties, essential oils, and" 
                                       + "\nprotein building blocks. We buy all of our acai from fairly traded \nand sustainable suppliers - meaning they pay a fair living wage to the \npeople of the Amazon that harvest the acai berry.";
                    }

                }                  
            }       
     
            scoreSummary.text = "Varun, Nick, Jay, and Alex";
            itemSummary.text = numOfItems + "/11 Ingredients Collected";
                foreach (KeyValuePair<Ingredient, int> item in inventory.itemsCollected) {
                    itemSummary.text += "\n" + item.Value + "/" + item.Key.points + " " + item.Key.name; 
                }

             if (numOfItems == 11) 
             {
                scoreSummary.text = "";         
                itemSummary.text = ""; 
                prompt.text = "Your bowl is now ready to make!" + "\nHead to Purple Bowl on 306B W Franklin St to collect your bowl";
             }    
     
    }

    
    public static string GetGameObjectPath(Ingredient obj)
    {
        string path = obj.name;
        return path;
    }
}
