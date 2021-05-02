using SellerScreen_2022.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SellerScreen_2022.Pages.Storage
{
    public class StorageData
    {
        public Dictionary<ulong, Product> Products = new Dictionary<ulong, Product>();
        public Dictionary<ulong, Product> Bin = new Dictionary<ulong, Product>();

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
                            if (Products.ContainsKey(product.Id))
                            {
                                Products[product.Id] = product;
                            }
                            else
                            {
                                Products.Add(product.Id, product);
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
                Data.Storage storage = new Data.Storage();
                foreach (KeyValuePair<ulong, Product> kvp in Products)
                {
                    storage.Products.Add(kvp.Value.Id);
                }

                foreach (KeyValuePair<ulong, Product> kvp in Bin)
                {
                    storage.Bin.Add(kvp.Value.Id);
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
                            if (Bin.ContainsKey(product.Id))
                            {
                                Bin[product.Id] = product;
                            }
                            else
                            {
                                Bin.Add(product.Id, product);
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
                Data.Storage storage = new Data.Storage();
                foreach (KeyValuePair<ulong, Product> kvp in Products)
                {
                    storage.Products.Add(kvp.Value.Id);
                }

                foreach (KeyValuePair<ulong, Product> kvp in Bin)
                {
                    storage.Bin.Add(kvp.Value.Id);
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

        public async Task<bool> RecycelProduct(ulong id)
        {
            try
            {
                Data.Storage storage = await Data.Storage.Load();
                Bin.Add(id, Products[id]);
                Products.Remove(id);
                storage.Products.Remove(id);
                storage.Bin.Add(id);
                await storage.Save();
            }
            catch (Exception ex)
            {
                await Errors.ShowErrorMsg(ex, "Storage", true);
                return false;
            }

            return true;
        }

        public async Task<bool> RestoreProduct(ulong id)
        {
            try
            {
                Data.Storage storage = await Data.Storage.Load();
                Products.Add(id, Bin[id]);
                Bin.Remove(id);
                storage.Bin.Remove(id);
                storage.Products.Add(id);
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

        public async Task<bool> DeleteProduct(ulong id)
        {
            try
            {
                File.Delete(Paths.productsPath + id.ToString() + ".xml");
                Bin.Remove(id);
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