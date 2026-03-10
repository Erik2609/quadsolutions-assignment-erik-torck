export function ErrorComponent({ errors }: { errors: string[] }) {
  return (
    <div>
      <h1>Errors:</h1>
      <ul>
        {errors.map((error, index) => (
          <li key={index}>{error}</li>
        ))}
      </ul>
      <button
        className="btn btn-warning"
        onClick={() => window.location.reload()}
      >
        Reload
      </button>
    </div>
  );
}
