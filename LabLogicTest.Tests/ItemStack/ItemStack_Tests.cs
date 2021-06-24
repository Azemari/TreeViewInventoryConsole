using System;
using System.Collections.Generic;
using LabLogicTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LabLogicTest.Tests.ItemStack
{
    [TestClass]
    public class Create_Tests
    {
        [TestMethod]
        public void CreateItemStack()
        {
            //Arrange
            List<Item> itemList = new List<Item>
            {
                new Item("name", Location.Ordered, "type"),
                new Item("name", Location.Ordered, "type"),
                new Item("name", Location.Ordered, "type"),
                new Item("name", Location.Ordered, "type")
            };

            //Act
            Models.ItemStack itemStack = new Models.ItemStack(1, Location.Ordered, "type", itemList);

            //Assert
            Assert.IsNotNull(itemStack);
            Assert.AreEqual(4, itemStack.Items.Count);
        }

        [TestMethod]
        public void MoveItemStack()
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

            //Act
            Models.ItemStack.Move(itemStack, Location.Received);

            //Assert
            Assert.AreEqual(Location.Received, itemStack.Location);
            Assert.AreEqual(Location.Received, itemStack.Items[0].Location);
            Assert.AreEqual(Location.Received, itemStack.Items[1].Location);
            Assert.AreEqual(Location.Received, itemStack.Items[2].Location);
            Assert.AreEqual(Location.Received, itemStack.Items[3].Location);
        }

        [TestMethod]
        public void UseOneItemFromBigItemStack()
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

            int initialCount = itemStack.Items.Count;

            //Act
            Models.ItemStack.UseOne(itemStack);

            //Assert
            Assert.AreEqual(initialCount - 1, itemStack.Items.Count);
        }

        [TestMethod]
        public void UseOneItemFromSmallItemStack()
        {
            //Arrange
            List<Item> itemList = new List<Item>
            {
                new Item("name", Location.Ordered, "type"),
            };
            Models.ItemStack itemStack = new Models.ItemStack(1, Location.Ordered, "type", itemList);

            int initialCount = itemStack.Items.Count;

            //Act
            Models.ItemStack.UseOne(itemStack);

            //Assert
            Assert.AreEqual(initialCount, itemStack.Items.Count);
            Assert.AreEqual(Location.Used, itemStack.Location);
        }
    }
}
