'use client'
const error = ({
    error,
    reset,
}:{
    error: Error,
    reset: () => void
}) => {
  return (
    <div>error <button onClick={reset}> TryAgain</button></div>
  )
}

export default error