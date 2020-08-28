logman start mysession -p {8ADA630A-F1CD-48BD-89F7-02CE2E7B9625} -o mytrace.etl -ets
logman stop mysession -ets
tracerpt mytrace.etl -o mytrace.xml -of XML