using SellerScreen_2022.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SellerScreen_2022.Pages.Storage
{
    public class StorageData
    {
        public Dictionary<string, Product> Products = new();
        public Dictionary<string, Product> Bin = new();

        public async Task<bool> LoadStorage()
        {
            try
            {
                Data.Storage storage = await Data.Storage.Load();
                if (storage != null)
                {
                    for (int i = 0; i < storage.Products.Count; i++)
                    {
                        try
                        {
                            Product product = await Product.Load(storage.Products[i]);
                            if (Products.ContainsKey(product.Key))
                            {
                                Products[product.Key] = product;
                            }
                            else
                            {
                                Products.Add(product.Key, product);
                            }
                        }
                        catch (Exception ex)
                        {
                            await Errors.ShowErrorMsg(ex, "Storage", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "Storage", true);
                return false;
            }

            return true;
        }

        public async Task<bool> SaveStorage()
        {
            try
            {
                Data.Storage storage = new();
                foreach (KeyValuePair<string, Product> kvp in Products)
                {
                    storage.Products.Add(kvp.Value.Key);
                }

                foreach (KeyValuePair<string, Product> kvp in Bin)
                {
                    storage.Bin.Add(kvp.Value.Key);
                }

                await storage.Save();
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "Storage", true);
                return false;
            }

            return true;
        }

        public async Task<bool> LoadBin()
        {
            try
            {
                Data.Storage storage = await Data.Storage.Load();
                if (storage != null)
                {
                    for (int i = 0; i < storage.Bin.Count; i++)
                    {
                        try
                        {
                            Product product = await Product.Load(storage.Bin[i]);
                            if (Bin.ContainsKey(product.Key))
                            {
                                Bin[product.Key] = product;
                            }
                            else
                            {
                                Bin.Add(product.Key, product);
                            }
                        }
                        catch (Exception ex)
                        {
                            await Errors.ShowErrorMsg(ex, "StorageBin", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "StorageBin", true);
                return false;
            }

            return true;
        }

        public async Task<bool> SaveBin()
        {
            try
            {
                Data.Storage storage = new();
                foreach (KeyValuePair<string, Product> kvp in Products)
                {
                    storage.Products.Add(kvp.Value.Key);
                }

                foreach (KeyValuePair<string, Product> kvp in Bin)
                {
                    storage.Bin.Add(kvp.Value.Key);
                }

                await storage.Save();
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "StorageBin", true);
                return false;
            }

            return true;
        }

        public async Task<bool> RecycelProduct(string key)
        {
            try
            {
                Data.Storage storage = await Data.Storage.Load();
                Bin.Add(key, Products[key]);
                Products.Remove(key);
                storage.Products.Remove(key);
                storage.Bin.Add(key);
                await storage.Save();
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "Storage", true);
                return false;
            }

            return true;
        }

        public async Task<bool> RestoreProduct(string key)
        {
            try
            {
                Data.Storage storage = await Data.Storage.Load();
                Products.Add(key, Bin[key]);
                Bin.Remove(key);
                storage.Bin.Remove(key);
                storage.Products.Add(key);
                await storage.Save();
                await SaveBin();
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "StorageBin", true);
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteProduct(string key)
        {
            try
            {
                File.Delete(Paths.productsPath + key.ToString() + ".xml");
                Bin.Remove(key);
                await SaveBin();
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "StorageBin", true);
                return false;
            }

            return true;
        }
    }
}