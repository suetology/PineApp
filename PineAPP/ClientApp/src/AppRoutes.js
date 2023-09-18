import { Home } from "./components/Home";
import { Browse } from "./components/Browse";
import { Create } from "./components/Create";
import {Study} from "./components/Study";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/browse',
    element: <Browse />
  },
  {
    path: '/create',
    element: <Create />
  },
  {
    path: '/study',
    element: <Study />
  }
];

export default AppRoutes;
