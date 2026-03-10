export function ErrorComponent({ errors }: { errors: string[] }) {
  return (
    <div>
      <h3>Errors:</h3>
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
