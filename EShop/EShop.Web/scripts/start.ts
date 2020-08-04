const childProcess = require("child_process");
const fs = require("fs");

function resetEnv() {
  const env = fs.readFileSync(".env.local", "utf8");
  const resevedEnvs = env
    .split("\n")
    .filter(
      (line) => Boolean(line) && !line.startsWith("REACT_APP_GIT_BRANCH")
    );

  // reset .env.local file and reseved other envs if any
  fs.writeFileSync(".env.local", resevedEnvs.join("\n") + '\n');
}

function writeToEnv(key: string, value: string) {
  fs.appendFileSync(".env.local", `${key}='${value.trim()}'\n`);
}

function getCurrentBranch() {
  return childProcess.execSync("git rev-parse --abbrev-ref HEAD").toString();
}

resetEnv();
writeToEnv("REACT_APP_GIT_BRANCH", getCurrentBranch());

// trick typescript to think it's a module
// https://stackoverflow.com/a/56577324/9449426
export {};
