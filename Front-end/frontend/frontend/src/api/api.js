const API_BASE_URL = "https://localhost:7048";

async function handleResponse(response, errorMessage) {
  if (!response.ok) {
    throw new Error(errorMessage);
  }

  if (response.status === 204) {
    return null;
  }

  return await response.json();
}

export async function getCategories() {
  const response = await fetch(`${API_BASE_URL}/Categories`);
  return handleResponse(response, "Could not fetch categories");
}

export async function createCategory(category) {
  const response = await fetch(`${API_BASE_URL}/Categories`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(category),
  });

  return handleResponse(response, "Could not create category");
}

export async function updateCategory(id, category) {
  const response = await fetch(`${API_BASE_URL}/Categories/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(category),
  });

  return handleResponse(response, "Could not update category");
}

export async function deleteCategory(id) {
  const response = await fetch(`${API_BASE_URL}/Categories/${id}`, {
    method: "DELETE",
  });

  return handleResponse(response, "Could not delete category");
}

export async function getMenuItems() {
  const response = await fetch(`${API_BASE_URL}/MenuItems`);
  return handleResponse(response, "Could not fetch menu items");
}

export async function createMenuItem(menuItem) {
  const response = await fetch(`${API_BASE_URL}/MenuItems`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(menuItem),
  });

  return handleResponse(response, "Could not create menu item");
}

export async function updateMenuItem(id, menuItem) {
  const response = await fetch(`${API_BASE_URL}/MenuItems/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(menuItem),
  });

  return handleResponse(response, "Could not update menu item");
}

export async function deleteMenuItem(id) {
  const response = await fetch(`${API_BASE_URL}/MenuItems/${id}`, {
    method: "DELETE",
  });

  return handleResponse(response, "Could not delete menu item");
}