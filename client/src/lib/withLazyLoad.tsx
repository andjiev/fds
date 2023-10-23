import { Suspense } from "react";
import Loading from "../components/Loading";

const withLazyLoad =
  <P extends object>(Component: React.ComponentType<P>) =>
    (props: P) =>
    (
      <Suspense
        fallback={
          <div style={{ minHeight: "100vh", display: "flex" }}>
            <Loading />
          </div>
        }
      >
        <Component {...props} />
      </Suspense>
    );

export default withLazyLoad;