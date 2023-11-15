import ClipLoader from "react-spinners/PropagateLoader";

function Loading() {

    return (
        <div data-testid="spinner">
            <ClipLoader
                loading={true}
                color={"#aed683"}
                cssOverride={{
                    display: "grid",
                    placeItems: "center",
                    height: "80vh"
                }}
                speedMultiplier={0.7}
                size={30}
                aria-label="Loading Spinner"
                data-testid="loader"
            />
        </div>
    );
}

export default Loading;