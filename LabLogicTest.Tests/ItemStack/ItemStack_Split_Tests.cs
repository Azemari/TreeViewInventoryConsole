using Microsoft.VisualStudio.TestTools.UnitTesting;
using LabLogicTest.Models;
using System.Collections.Generic;

namespace LabLogicTest.Tests.ItemStack
{
    [TestClass]
    public class ItemStack_Split_Tests
    {
        [TestMethod]
        public void Into2EquallySizedStacks()
        {
            //Arrange
            List<Item> itemList = new List<Item>
            {
                new Item("name", Location.Used, "type"),
                new Item("name", Location.Used, "type"),
                new Item("name", Location.Used, "type"),
                new Item("name", Location.Used, "type")
            };
            Models.ItemStack itemStack = new Models.ItemStack(1, Location.Ordered, "type", itemList);

            //Act
            Models.ItemStack newItemStack = Models.ItemStack.Split(itemStack, 2);

            //Assert
            Assert.AreEqual(2, itemStack.Items.Count);
            Assert.AreEqual(2, newItemStack.Items.Count);
        }

        [TestMethod]
        public void Into2DifferentSizedStacks()
        {
            //Arrange
            List<Item> itemList = new List<Item>
            {
                new Item("name", Location.Used, "type"),
                new Item("name", Location.Used, "type"),
                new Item("name", Location.Used, "type"),
                new Item("name", Location.Used, "type")
            };
            Models.ItemStack itemStack = new Models.ItemStack(1, Location.Ordered, "type", itemList);

            //Act
            Models.ItemStack newItemStack = Models.ItemStack.Split(itemStack, 1);

            //Assert
            Assert.AreEqual(3, itemStack.Items.Count);
            Assert.AreEqual(1, newItemStack.Items.Count);
        }

        [TestMethod]
        public void LargerThanQuantity()
        {
            //Arrange
            List<Item> itemList = new List<Item>
            {
                new Item("name", Location.Used, "type"),
                new Item("name", Location.Used, "type"),
                new Item("name", Location.Used, "type"),
                new Item("name", Location.Used, "type")
            };
            Models.ItemStack itemStack = new Models.ItemStack(1, Location.Ordered, "type", itemList);

            int initialStackSize = 4;

            //Act
            Models.ItemStack newItemStack = Models.ItemStack.Split(itemStack, 10);

            //Assert
            Assert.AreEqual(initialStackSize, itemStack.Items.Count);
            Assert.IsNull(newItemStack);
        }
    }
}
