import { useEffect, useState } from "react";

const MENU_ITEMS_URL = "https://localhost:7048/api/MenuItems";
const CATEGORIES_URL = "https://localhost:7048/api/Categories";

function MenuItems() {
  const [menuItems, setMenuItems] = useState([]);
  const [categories, setCategories] = useState([]);

  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [price, setPrice] = useState("");
  const [categoryId, setCategoryId] = useState("");

  const [editingItemId, setEditingItemId] = useState(null);
  const [editingName, setEditingName] = useState("");
  const [editingDescription, setEditingDescription] = useState("");
  const [editingPrice, setEditingPrice] = useState("");
  const [editingCategoryId, setEditingCategoryId] = useState("");
  const [editingIsAvailable, setEditingIsAvailable] = useState(true);

  const [error, setError] = useState("");

  useEffect(() => {
    loadMenuItems();
    loadCategories();
  }, []);

  async function loadMenuItems() {
    try {
      const response = await fetch(MENU_ITEMS_URL);

      if (!response.ok) {
        throw new Error("Could not fetch menu items");
      }

      const data = await response.json();
      setMenuItems(data);
    } catch (error) {
      console.error(error);
      setError("Could not connect to menu items API");
    }
  }

  async function loadCategories() {
    try {
      const response = await fetch(CATEGORIES_URL);

      if (!response.ok) {
        throw new Error("Could not fetch categories");
      }

      const data = await response.json();
      setCategories(data);
    } catch (error) {
      console.error(error);
      setError("Could not connect to categories API");
    }
  }

  async function handleCreate(event) {
    event.preventDefault();

    if (!name.trim()) {
      setError("Menu item name is required");
      return;
    }

    if (!description.trim()) {
      setError("Description is required");
      return;
    }

    if (Number(price) <= 0) {
      setError("Price must be greater than zero");
      return;
    }

    if (!categoryId) {
      setError("Category is required");
      return;
    }

    try {
      const response = await fetch(MENU_ITEMS_URL, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          name,
          description,
          price: Number(price),
          categoryId: Number(categoryId),
        }),
      });

      if (!response.ok) {
        throw new Error("Could not create menu item");
      }

      setName("");
      setDescription("");
      setPrice("");
      setCategoryId("");
      setError("");

      await loadMenuItems();
    } catch (error) {
      console.error(error);
      setError("Could not create menu item");
    }
  }

  function startEdit(item) {
    setEditingItemId(item.id);
    setEditingName(item.name);
    setEditingDescription(item.description);
    setEditingPrice(item.price);
    setEditingIsAvailable(item.isAvailable);

    const matchingCategory = categories.find(
      (category) => category.name === item.categoryName
    );

    setEditingCategoryId(matchingCategory ? matchingCategory.id : "");
  }

  function cancelEdit() {
    setEditingItemId(null);
    setEditingName("");
    setEditingDescription("");
    setEditingPrice("");
    setEditingCategoryId("");
    setEditingIsAvailable(true);
  }

  async function handleUpdate(id) {
    if (!editingName.trim()) {
      setError("Menu item name is required");
      return;
    }

    if (!editingDescription.trim()) {
      setError("Description is required");
      return;
    }

    if (Number(editingPrice) <= 0) {
      setError("Price must be greater than zero");
      return;
    }

    if (!editingCategoryId) {
      setError("Category is required");
      return;
    }

    try {
      const response = await fetch(`${MENU_ITEMS_URL}/${id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          name: editingName,
          description: editingDescription,
          price: Number(editingPrice),
          isAvailable: editingIsAvailable,
          categoryId: Number(editingCategoryId),
        }),
      });

      if (!response.ok) {
        throw new Error("Could not update menu item");
      }

      cancelEdit();
      setError("");
      await loadMenuItems();
    } catch (error) {
      console.error(error);
      setError("Could not update menu item");
    }
  }

  async function handleDelete(id) {
    const confirmed = window.confirm("Are you sure you want to delete this menu item?");

    if (!confirmed) {
      return;
    }

    try {
      const response = await fetch(`${MENU_ITEMS_URL}/${id}`, {
        method: "DELETE",
      });

      if (!response.ok) {
        throw new Error("Could not delete menu item");
      }

      setError("");
      await loadMenuItems();
    } catch (error) {
      console.error(error);
      setError("Could not delete menu item");
    }
  }

  return (
    <div>
      <h1>Menu Items</h1>

      {error && <p className="error">{error}</p>}

      <form onSubmit={handleCreate} className="form">
        <input
          type="text"
          placeholder="Menu item name"
          value={name}
          onChange={(event) => setName(event.target.value)}
        />

        <input
          type="text"
          placeholder="Description"
          value={description}
          onChange={(event) => setDescription(event.target.value)}
        />

        <input
          type="number"
          placeholder="Price"
          value={price}
          onChange={(event) => setPrice(event.target.value)}
        />

        <select
          value={categoryId}
          onChange={(event) => setCategoryId(event.target.value)}
        >
          <option value="">Select category</option>

          {categories.map((category) => (
            <option key={category.id} value={category.id}>
              {category.name}
            </option>
          ))}
        </select>

        <button type="submit">Add Menu Item</button>
      </form>

      {menuItems.length === 0 ? (
        <p>No menu items found.</p>
      ) : (
        <ul className="list">
          {menuItems.map((item) => (
            <li key={item.id} className="list-item">
              {editingItemId === item.id ? (
                <>
                  <input
                    type="text"
                    value={editingName}
                    onChange={(event) => setEditingName(event.target.value)}
                  />

                  <input
                    type="text"
                    value={editingDescription}
                    onChange={(event) => setEditingDescription(event.target.value)}
                  />

                  <input
                    type="number"
                    value={editingPrice}
                    onChange={(event) => setEditingPrice(event.target.value)}
                  />

                  <select
                    value={editingCategoryId}
                    onChange={(event) => setEditingCategoryId(event.target.value)}
                  >
                    <option value="">Select category</option>

                    {categories.map((category) => (
                      <option key={category.id} value={category.id}>
                        {category.name}
                      </option>
                    ))}
                  </select>

                  <label>
                    Available
                    <input
                      type="checkbox"
                      checked={editingIsAvailable}
                      onChange={(event) =>
                        setEditingIsAvailable(event.target.checked)
                      }
                    />
                  </label>

                  <button onClick={() => handleUpdate(item.id)}>Save</button>
                  <button onClick={cancelEdit}>Cancel</button>
                </>
              ) : (
                <>
                  <div>
                    <strong>{item.name}</strong>
                    <p>{item.description}</p>
                    <p>Price: {item.price} kr</p>
                    <p>Category: {item.categoryName}</p>
                    <p>{item.isAvailable ? "Available" : "Not available"}</p>
                  </div>

                  <div>
                    <button onClick={() => startEdit(item)}>Edit</button>
                    <button onClick={() => handleDelete(item.id)}>Delete</button>
                  </div>
                </>
              )}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}

export default MenuItems;