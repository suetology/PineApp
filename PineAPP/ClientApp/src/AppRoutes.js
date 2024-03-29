import Home from "./components/Home";
import Browse from "./components/Browse";
import Create from "./components/Create";
import Study from "./components/Study";
import CreateDeck from "./components/CreateDeck"
import RegisterPage from "./components/RegisterPage.jsx";
import LoginPage from './components/LoginPage';

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
    path: '/create/:id',
    element: <Create />
  },
  {
    path: '/study/:id',
    element: <Study />
  },
  {
    path : '/create',
    element : <CreateDeck/>
  },
  {
    path: '/login',
    element: <LoginPage/>
  },
  {
    path: '/register',
    element: <RegisterPage/>
  }
];

export default AppRoutes;
