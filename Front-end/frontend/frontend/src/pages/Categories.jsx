import { useEffect, useState } from "react";

const API_URL = "https://localhost:7048/api/Categories";

function Categories() {
  const [categories, setCategories] = useState([]);
  const [name, setName] = useState("");
  const [editingCategoryId, setEditingCategoryId] = useState(null);
  const [editingName, setEditingName] = useState("");
  const [error, setError] = useState("");

  useEffect(() => {
    loadCategories();
  }, []);

  async function loadCategories() {
    try {
      const response = await fetch(API_URL);

      if (!response.ok) {
        throw new Error("Could not fetch categories");
      }

      const data = await response.json();
      setCategories(data);
    } catch (error) {
      console.error(error);
      setError("Could not connect to API");
    }
  }

  async function handleCreate(event) {
    event.preventDefault();

    if (!name.trim()) {
      setError("Category name is required");
      return;
    }

    try {
      const response = await fetch(API_URL, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ name }),
      });

      if (!response.ok) {
        throw new Error("Could not create category");
      }

      setName("");
      setError("");
      await loadCategories();
    } catch (error) {
      console.error(error);
      setError("Could not create category");
    }
  }

  async function handleUpdate(id) {
    if (!editingName.trim()) {
      setError("Category name is required");
      return;
    }

    try {
      const response = await fetch(`${API_URL}/${id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ name: editingName }),
      });

      if (!response.ok) {
        throw new Error("Could not update category");
      }

      setEditingCategoryId(null);
      setEditingName("");
      setError("");
      await loadCategories();
    } catch (error) {
      console.error(error);
      setError("Could not update category");
    }
  }

  async function handleDelete(id) {
    const confirmed = window.confirm("Are you sure you want to delete this category?");

    if (!confirmed) {
      return;
    }

    try {
      const response = await fetch(`${API_URL}/${id}`, {
        method: "DELETE",
      });

      if (!response.ok) {
        throw new Error("Could not delete category");
      }

      setError("");
      await loadCategories();
    } catch (error) {
      console.error(error);
      setError("Could not delete category");
    }
  }

  function startEdit(category) {
    setEditingCategoryId(category.id);
    setEditingName(category.name);
  }

  function cancelEdit() {
    setEditingCategoryId(null);
    setEditingName("");
  }

  return (
    <div>
      <h1>Categories</h1>

      {error && <p className="error">{error}</p>}

      <form onSubmit={handleCreate} className="form">
        <input
          type="text"
          placeholder="Category name"
          value={name}
          onChange={(event) => setName(event.target.value)}
        />

        <button type="submit">Add Category</button>
      </form>

      {categories.length === 0 ? (
        <p>No categories found.</p>
      ) : (
        <ul className="list">
          {categories.map((category) => (
            <li key={category.id} className="list-item">
              {editingCategoryId === category.id ? (
                <>
                  <input
                    type="text"
                    value={editingName}
                    onChange={(event) => setEditingName(event.target.value)}
                  />

                  <button onClick={() => handleUpdate(category.id)}>Save</button>
                  <button onClick={cancelEdit}>Cancel</button>
                </>
              ) : (
                <>
                  <span>
                    {category.name} - {category.totalMenuItems} items
                  </span>

                  <div>
                    <button onClick={() => startEdit(category)}>Edit</button>
                    <button onClick={() => handleDelete(category.id)}>Delete</button>
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

export default Categories;