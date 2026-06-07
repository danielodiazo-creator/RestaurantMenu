import { Link } from "react-router-dom";

function Navbar() {
  return (
    <nav className="navbar">
      <h2>Restaurant Menu</h2>

      <div>
        <Link to="/">Home</Link>
        <Link to="/categories">Categories</Link>
        <Link to="/menu-items">Menu Items</Link>
      </div>
    </nav>
  );
}

export default Navbar;