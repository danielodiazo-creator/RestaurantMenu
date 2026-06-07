import { BrowserRouter, Routes, Route } from "react-router-dom";
import Navbar from "./components/Navbar";
import Home from "./pages/Home";
import Categories from "./pages/Categories";
import MenuItems from "./pages/MenuItems";
import "./index.css";

function App() {
  return (
    <BrowserRouter>
      <Navbar />

      <main className="container">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/categories" element={<Categories />} />
          <Route path="/menu-items" element={<MenuItems />} />
        </Routes>
      </main>
    </BrowserRouter>
  );
}

export default App;