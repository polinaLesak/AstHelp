import { BrowserRouter } from "react-router-dom";
import AppRoutes from "./app/AppRoutes";
import ErrorToast from "./shared/components/ErrorToast";
import Footer from "./shared/components/Footer";
import LoadingOverlay from "./shared/components/LoadingOverlay";
import Navbar from "./shared/components/Navbar";

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <LoadingOverlay />
        <ErrorToast />
        <div
          style={{
            display: "flex",
            flexDirection: "column",
            minHeight: "100vh",
          }}
        >
          <Navbar />
          <div style={{ flex: "1 0 auto", paddingTop: "64px" }}>
            <AppRoutes />
          </div>
          <Footer />
        </div>
        </BrowserRouter>
    </div>
  );
}

export default App;
