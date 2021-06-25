using System;
using System.Collections.Generic;
using LabLogicTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LabLogicTest.Tests
{
    [TestClass]
    public class ItemStack_Tests
    {
        [TestMethod]
        public void Delete_ItemStack_InRoot()
        {
            //Arrange
            RootNode availableToOrder = new RootNode(Location.AvailableToOrder);

            ItemStack itemStack = new ItemStack("Test", Location.AvailableToOrder, null, availableToOrder, "Type", 1);
            ItemStack itemStack2 = new ItemStack("Test", Location.AvailableToOrder, null, availableToOrder, "Type", 2);
            ItemStack nestedItemStack = new ItemStack("Test", Location.AvailableToOrder, null, availableToOrder, "Type", 1);
            Folder folder = new Folder("Test", Location.AvailableToOrder, null, availableToOrder, new List<BaseItem>());

            availableToOrder.Nodes.Add(itemStack);
            availableToOrder.Nodes.Add(itemStack2);
            availableToOrder.Nodes.Add(nestedItemStack);
            availableToOrder.Nodes.Add(folder);

            Guid deletedItemStackId = itemStack.Id;

            //Act
            availableToOrder.Delete(itemStack);

            //Assert
            Assert.AreEqual(3, availableToOrder.Nodes.Count);
            Assert.AreEqual(0, folder.Nodes.Count);
            Assert.AreEqual(deletedItemStackId, itemStack.Id);
        }

        [TestMethod]
        public void Delete_ItemStack_InFolder()
        {
            //Arrange
            RootNode availableToOrder = new RootNode(Location.AvailableToOrder);

            ItemStack itemStack = new ItemStack("Test", Location.AvailableToOrder, null, availableToOrder, "Type", 1);
            ItemStack nestedItemStack = new ItemStack("Test", Location.AvailableToOrder, null, availableToOrder, "Type", 1);
            Folder folder = new Folder("Test", Location.AvailableToOrder, null, availableToOrder, new List<BaseItem>());

            availableToOrder.Nodes.Add(itemStack);
            availableToOrder.Nodes.Add(folder);
            folder.Nodes.Add(nestedItemStack);

            Guid deletedItemStackId = nestedItemStack.Id;

            //Act
            availableToOrder.Delete(nestedItemStack);

            //Assert
            Assert.AreEqual(2, availableToOrder.Nodes.Count);
            Assert.AreEqual(0, folder.Nodes.Count);
            Assert.AreEqual(deletedItemStackId, nestedItemStack.Id);
        }

        [TestMethod]
        public void Move_ItemStackIntoFolder()
        {
            //Arrange
            RootNode availableToOrder = new RootNode(Location.AvailableToOrder);

            ItemStack itemStack = new ItemStack("Test", Location.AvailableToOrder, null, availableToOrder, "Type", 1);
            ItemStack nestedItemStack = new ItemStack("Test", Location.AvailableToOrder, null, availableToOrder, "Type", 1);
            Folder folder = new Folder("Test", Location.AvailableToOrder, null, availableToOrder, new List<BaseItem>());

            availableToOrder.Nodes.Add(itemStack);
            availableToOrder.Nodes.Add(nestedItemStack);
            availableToOrder.Nodes.Add(folder);

            Guid movedFolderId = nestedItemStack.Id;

            //Act
            nestedItemStack.MoveTo(folder);

            //Assert
            Assert.AreEqual(2, availableToOrder.Nodes.Count);
            Assert.AreEqual(1, folder.Nodes.Count);
            Assert.AreEqual(movedFolderId, folder.Nodes[0].Id);
        }

        [TestMethod]
        public void Move_NestedItemStackIntoCategory()
        {
            //Arrange
            RootNode availableToOrder = new RootNode(Location.AvailableToOrder);
            RootNode ordered = new RootNode(Location.Ordered);

            ItemStack itemStack = new ItemStack("Test", Location.AvailableToOrder, null, availableToOrder, "Type", 1);
            Folder folder = new Folder("Test", Location.AvailableToOrder, null, availableToOrder, new List<BaseItem>());
            ItemStack nestedItemStack = new ItemStack("Test", Location.AvailableToOrder, folder, availableToOrder, "Type", 1);

            availableToOrder.Nodes.Add(itemStack);
            availableToOrder.Nodes.Add(folder);
            folder.Nodes.Add(nestedItemStack);

            //Act
            nestedItemStack.MoveTo(ordered);

            //Assert
            Assert.AreEqual(0, folder.Nodes.Count);
            Assert.AreEqual(1, ordered.Nodes.Count);
        }

        [TestMethod]
        public void Move_RootItemStackIntoCategory()
        {
            //Arrange
            RootNode availableToOrder = new RootNode(Location.AvailableToOrder);
            RootNode ordered = new RootNode(Location.Ordered);

            ItemStack itemStack = new ItemStack("Test", Location.AvailableToOrder, null, availableToOrder, "Type", 1);

            availableToOrder.Nodes.Add(itemStack);

            //Act
            itemStack.MoveTo(ordered);

            //Assert
            Assert.AreEqual(0, availableToOrder.Nodes.Count);
            Assert.AreEqual(1, ordered.Nodes.Count);
        }

        [TestMethod]
        public void UseOneItemFromBigItemStack()
        {
            //Arrange
            RootNode stored = new RootNode(Location.Stored);
            RootNode used = new RootNode(Location.Used);
            ItemStack itemStack = new ItemStack("Test", Location.Stored, null, stored, "Type", 5);
            stored.Nodes.Add(itemStack);

            int startingQuantity = itemStack.Quantity;
            int startingStoredNodeCount = stored.Nodes.Count;
            int startingUsedNodeCount = used.Nodes.Count;

            //Act
            itemStack.UseOne(used);

            //Assert
            Assert.AreEqual(startingQuantity - 1, itemStack.Quantity);
            Assert.AreEqual(startingStoredNodeCount, stored.Nodes.Count);
            Assert.AreEqual(startingUsedNodeCount + 1, used.Nodes.Count);
        }

        [TestMethod]
        public void UseOneItemFromSmallItemStack()
        {
            //Arrange
            RootNode stored = new RootNode(Location.Stored);
            RootNode used = new RootNode(Location.Used);
            ItemStack itemStack = new ItemStack("Test", Location.Stored, null, stored, "Type", 1);
            stored.Nodes.Add(itemStack);

            int startingStoredNodeCount = stored.Nodes.Count;
            int startingUsedNodeCount = used.Nodes.Count;

            //Act
            itemStack.UseOne(used);

            //Assert
            Assert.AreEqual(startingStoredNodeCount - 1, stored.Nodes.Count);
            Assert.AreEqual(startingUsedNodeCount + 1, used.Nodes.Count);
        }

        [TestMethod]
        public void Move_NestedFolderToRoot()
        {
            //Arrange
            RootNode availableToOrder = new RootNode(Location.AvailableToOrder);

            ItemStack itemStack = new ItemStack("Test", Location.AvailableToOrder, null, availableToOrder, "Type", 1);
            Folder folder = new Folder("Test", Location.AvailableToOrder, null, availableToOrder, new List<BaseItem>());
            Folder nestedfolder = new Folder("Test", Location.AvailableToOrder, folder, availableToOrder, new List<BaseItem>());
            ItemStack nestedItemStack = new ItemStack("Test", Location.AvailableToOrder, nestedfolder, availableToOrder, "Type", 1);

            availableToOrder.Nodes.Add(itemStack);
            availableToOrder.Nodes.Add(folder);
            folder.Nodes.Add(nestedfolder);
            nestedfolder.Nodes.Add(nestedItemStack);

            //Act
            nestedfolder.MoveTo(availableToOrder);

            //Assert
            Assert.AreEqual(3, availableToOrder.Nodes.Count);
            Assert.AreEqual(0, folder.Nodes.Count);
            Assert.AreEqual(1, nestedfolder.Nodes.Count);
            Assert.AreEqual(Location.AvailableToOrder, nestedfolder.Nodes[0].Location);
        }

        [TestMethod]
        public void Nest_Folder()
        {
            //Arrange
            RootNode availableToOrder = new RootNode(Location.AvailableToOrder);

            ItemStack itemStack = new ItemStack("Test", Location.AvailableToOrder, null, availableToOrder, "Type", 1);
            Folder folder = new Folder("Test", Location.AvailableToOrder, null, availableToOrder, new List<BaseItem>());
            Folder folderToNest = new Folder("Test", Location.AvailableToOrder, null, availableToOrder, new List<BaseItem>());
            ItemStack nestedItemStack = new ItemStack("Test", Location.AvailableToOrder, folderToNest, availableToOrder, "Type", 1);

            availableToOrder.Nodes.Add(itemStack);
            availableToOrder.Nodes.Add(folder);
            availableToOrder.Nodes.Add(folderToNest);
            folderToNest.Nodes.Add(nestedItemStack);

            //Act
            folderToNest.MoveTo(folder);

            //Assert
            Assert.AreEqual(2, availableToOrder.Nodes.Count);
            Assert.AreEqual(1, folder.Nodes.Count);
            Assert.AreEqual(1, folderToNest.Nodes.Count);
            Assert.AreEqual(Location.AvailableToOrder, folderToNest.Nodes[0].Location);
        }

        [TestMethod]
        public void Split_ItemStackInto2()
        {
            //Arrange
            RootNode used = new RootNode(Location.Used);
            ItemStack toSplitItemStack = new ItemStack("Test", Location.Used, null, used, "Type", 5);
            used.Nodes.Add(toSplitItemStack);

            int startingQuantity = toSplitItemStack.Quantity;

            //Act
            toSplitItemStack.Split(3);

            //Assert
            Assert.AreEqual(2, used.Nodes.Count);
            ItemStack itemStack1 = (ItemStack)used.Nodes[0];
            ItemStack itemStack2 = (ItemStack)used.Nodes[1];
            Assert.AreEqual(startingQuantity, itemStack1.Quantity + itemStack2.Quantity);
        }

        [TestMethod]
        public void Split_ItemStackIntoInvalidStack()
        {
            //Arrange
            RootNode used = new RootNode(Location.Used);
            ItemStack toSplitItemStack = new ItemStack("Test", Location.Used, null, used, "Type", 5);
            used.Nodes.Add(toSplitItemStack);

            int startingQuantity = toSplitItemStack.Quantity;

            //Act
            toSplitItemStack.Split(13);

            //Assert
            Assert.AreEqual(1, used.Nodes.Count);
            ItemStack itemStack = (ItemStack)used.Nodes[0];
            Assert.AreEqual(startingQuantity, itemStack.Quantity = startingQuantity);
        }

        [TestMethod]
        public void Merge_ItemStack()
        {
            //Arrange
            RootNode used = new RootNode(Location.Used);
            ItemStack mergeItemStack = new ItemStack("Test", Location.Used, null, used, "Type", 1);
            ItemStack mergedItemStack = new ItemStack("Test", Location.Used, null, used, "Type", 1);
            used.Nodes.Add(mergeItemStack);
            used.Nodes.Add(mergedItemStack);

            int startingQuantity = mergeItemStack.Quantity + mergedItemStack.Quantity;

            //Act
            mergeItemStack.Merge(mergedItemStack);

            //Assert
            Assert.AreEqual(1, used.Nodes.Count);
            Assert.AreEqual(startingQuantity, mergedItemStack.Quantity);
        }
    }
}
