using System.Collections.Generic;
using LabLogicTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LabLogicTest.Tests.Folder
{
    [TestClass]
    public class Folder_Tests
    {
        [TestMethod]
        public void Create_Folder()
        {
            //Arrange
            List<Item> itemList = new List<Item>
            {
                new Item("name", Location.Ordered, "type"),
                new Item("name", Location.Ordered, "type"),
                new Item("name", Location.Ordered, "type"),
                new Item("name", Location.Ordered, "type")
            };
            Models.ItemStack itemStack = new Models.ItemStack(1, Location.Ordered, "type", itemList);
            List<Models.ItemStack> itemStacks = new List<Models.ItemStack>
            {
                itemStack
            };

            //Act
            Models.Folder folder = new Models.Folder("testName", Location.Ordered, itemStacks);

            //Assert
            Assert.IsNotNull(folder);
            Assert.AreEqual(1, folder.ItemStacks.Count);
            Assert.AreEqual(4, folder.ItemStacks[0].Items.Count);
        }

        [TestMethod]
        public void Move_Folder()
        {
            List<Item> itemList = new List<Item>
            {
                new Item("name", Location.Ordered, "type"),
                new Item("name", Location.Ordered, "type"),
                new Item("name", Location.Ordered, "type"),
                new Item("name", Location.Ordered, "type")
            };
            Models.ItemStack itemStack = new Models.ItemStack(1, Location.Ordered, "type", itemList);
            List<Models.ItemStack> itemStacks = new List<Models.ItemStack>
            {
                itemStack
            };
            Models.Folder folder = new Models.Folder("testName", Location.Ordered, itemStacks);

            //Act
            Models.Folder.MoveFolder(folder, Location.Used);

            //Assert
            Assert.AreEqual(Location.Used, folder.Location);
            Assert.AreEqual(Location.Used, folder.ItemStacks[0].Location);
            Assert.AreEqual(Location.Used, folder.ItemStacks[0].Items[0].Location);
            Assert.AreEqual(Location.Used, folder.ItemStacks[0].Items[1].Location);
            Assert.AreEqual(Location.Used, folder.ItemStacks[0].Items[2].Location);
            Assert.AreEqual(Location.Used, folder.ItemStacks[0].Items[3].Location);
        }
    }
}
